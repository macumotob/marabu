using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Collections.Specialized;
using System.Web;


namespace marabu
{
    public class CobaServer : Server
    {
        public event EventWSUserStateChanged OnWSUserStateChanged;


        private readonly string[] _indexFiles = {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm" ,
      "/js/bootstrap.min.js",
      "/css/bootstrap.min.css"
    };

        //private Thread _serverThread;
        private string _webAppDirectory;

        private HttpListener _listener;
        private Exception _exception;

        public int Port
        {
            get { return _port; }
            private set { }
        }

        public override string Title
        {
            get
            {
                return "Marabu";
            }
        }

        public CobaServer()
        {
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public CobaServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(path, port);
        }
        private void _raiseUserChanged(WSClientInfo info)
        {
            if (this.OnWSUserStateChanged != null)
            {
                this.OnWSUserStateChanged(info);
            }
        }

        private string _commandMenuItemId { get; set; }
        private string _commandFieldId { get; set; }



        private bool _full_log_mode = true;
        //public bool Run(string host,int port,string menuId,string fieldId,string  cisco)
        private void _SetWebSocketHostPort()
        {
            string file =AppDomain.CurrentDomain.BaseDirectory + "\\webapp\\js\\loader.js";
            string text = System.IO.File.ReadAllText(file);
            text = text.Replace("##ADRESS##", _host.ToString());
            text = text.Replace("##PORT##", _port.ToString());
            File.WriteAllText(file, text);
        }
        public bool Run(ServerSettings settings)
        {
            _host = settings.MarabuHost;
            _port = settings.iMarabuPort;

            _SetWebSocketHostPort();
            _commandMenuItemId = settings.MenuItemId;
            _commandFieldId = settings.FieldId;

            _full_log_mode = settings.FullLogMode;

            if (_isWorking)
            {
                //        RaiseEvent("Already Running on host: " + _host + " port: " + _port.ToString());
                RaiseStopped();
                _isWorking = false;
                return false;
            }
            IPAddress address = _findAddress(_host, _port);//IPAddress.Parse(host);
            if (address == null)
            {
                //RaiseEvent("Error resolve host: " + _host + " port: " + _port.ToString());
                Manager.Log.Log("Server error . can't resolve host {0} port{1}", _host, _port);
                RaiseStopped();
                return false;
            }
            this.Initialize(AppDomain.CurrentDomain.BaseDirectory + "webapp/", _port);
            return true;
        }

        public void SetMessage(string message)
        {
            lock (_lockObject)
            {
                foreach (var user in _users)
                {
                    if (user.CanReceiveMessage)
                    {
                        user.Message = message;
                    }
                }
            }
        }
        public void SetHTTPMessage(string userName, string fieldValue)
        {
            lock (_lockObject)
            {
                string message = Manager.MakeHTTPMessage(_commandMenuItemId, _commandFieldId, fieldValue);
                foreach (var user in _users)
                {
                    if (user.UserName == userName)
                    {
                        user.CommandFromHTTP = message;
                    }
                }
            }
        }
        public void SendAlerting(string userName, string status, string fieldValue)
        {
            lock (_lockObject)
            {
                string message = Manager.MakeHTTPMessage(_commandMenuItemId, _commandFieldId, fieldValue);
                foreach (var user in _users)
                {
                    if (user.UserName == userName)
                    {
                        if (user.CallStatus == null && status == "ALERTING")
                        {
                            user.CallStatus = status;
                            user.CommandFromHTTP = message;
                        }
                    }
                }
            }
        }

        public void _clearUserList()
        {
            lock (_lockObject)
            {
                _users.Clear();

            }
        }

        private void Listen()
        {

            _listener = new HttpListener();
            //_listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://localhost:" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://192.168.1.5:" + _port.ToString() + "/");
            //	_listener.Prefixes.Add("http://+:" + _port.ToString() + "/");
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/", _host, _port));
            _listener.Start();
            _listener.IgnoreWriteExceptions = true;

            //this.RaiseEvent("SERVER host :" + _host + " port: " + _port.ToString());
            Manager.Log.Log("Server main loop start. Host : {0} port {1}", _host, _port);
            SocketClient client;
            _task = Task.Factory.StartNew(() =>
             {
                 while (_listener.IsListening)
                 {

                     HttpListenerContext context = _listener.GetContext();
                     Thread.Sleep(_sleepTime);
               //if (!_isWorking)
               //{
               //  Process((HttpListenerContext)context);
               //  break;
               //}
               if (context.Request.IsWebSocketRequest)
                     {
                         Task.Factory.StartNew(() =>
                   {
                       client = new SocketClient(this, _commandMenuItemId, _commandFieldId, _full_log_mode);
                       client.Execute(context);
                   }, TaskCreationOptions.LongRunning);
                     }
                     else
                     {
                         bool needStopServer = Process((HttpListenerContext)context);
                         if (needStopServer) break;
                     }
                 }
                 Manager.Log.Log("Server main loop end. Host {0} port {1}.", _host, _port);
             }, TaskCreationOptions.LongRunning).ContinueWith((t) =>
               {
                   if (_listener.IsListening)
                   {
                       _listener.Stop();
                   }
               });
        }

        private void _sendStopListener()
        {
            WebClient client = new WebClient();
            try
            {
                Uri uri = new Uri(string.Format("http://{0}:{1}/_stop_server_from_admin_", _host, _port));
                //client.DownloadStringAsync(uri);
                string s = client.DownloadString(uri);
                client.Dispose();
                client = null;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
        }
        private bool _stopListener()
        {
            try
            {
                if (_listener.IsListening)
                {
                    _sendStopListener();
                    Thread.Sleep(1000);
                    _listener.Stop();
                }
                Thread.Sleep(100);
                if (_listener.IsListening)
                {
                    _listener.Abort();
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
            return !_listener.IsListening;
        }
        public override void Stop()
        {
            if (!_is_working)
            {
                return;
            }
            _is_working = false;
            _clearUserList();

            try
            {
                bool result = _stopListener();
                Thread.Sleep(100);
                if (!_task.IsCompleted)
                {
                    result = _task.Wait(5000);
                }
                _task.Dispose();
                _task = null;
                GC.Collect();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                {
                    string text = e.Message + " " + v.Message;
                }

            }
            finally
            {
                if (_task != null)
                {
                    _task.Dispose();
                    _task = null;
                }
                //   _tokenSource.Dispose();
                //   _tokenSource = null;
            }
            this.RaiseStopped();
        }


        public override string ToString()
        {
            return string.Format("Host {0} Port: {1}", _host, _port);
        }
        public string ToStringURL()
        {
            return string.Format("http://{0}:{1}/", _host, _port);
        }
        /*
        private bool _ciscoGetDialogs()
        {
          if (!_isWorking) return false;

          CiscoClient client = new CiscoClient("2002");
          client.GetDialogs(this);

          return _isWorking;
        }
        */
        private CobaClient _createClient()
        {
            return new CobaClient(_webAppDirectory);
        }

        private List<WSClientInfo> _users = new List<WSClientInfo>();
        public WSClientInfo FindUser(string userName, string win)
        {
            lock (_lockObject)
            {
                foreach (var user in _users)
                {
                    if (user.UserName == userName && user.WIN == win)
                    {
                        return user;
                    }
                }
            }
            return null;
        }
        public void ProcessUser(WSClientStatus command, string userName, string win)
        {
            lock (_lockObject)
            {
                foreach (var user in _users)
                {
                    if (user.UserName == userName && user.WIN == win)
                    {
                        user.Pulse++;
                        user.Command = command;

                        _raiseUserChanged(user);
                        return;
                    }
                }
                if (command == WSClientStatus.tapi)
                {
                    command = WSClientStatus.tapi_reg;
                }
                WSClientInfo info = new WSClientInfo() { UserName = userName, WIN = win, Command = command, Pulse = 1 };
                _users.Add(info);
                _raiseUserChanged(info);
                return;
            }
        }
        public void SetCanReceiveMessage(string userName, string win, bool can)
        {
            lock (_lockObject)
            {
                WSClientInfo user = FindUser(userName, win);
                if (user != null)
                {
                    user.CanReceiveMessage = can;
                }
            }
        }
        public WSClientInfo RegisterUser(string userName, string win)
        {
            lock (_lockObject)
            {
                WSClientInfo info = new WSClientInfo() { UserName = userName, WIN = win, Command = WSClientStatus.reg, Pulse = 1 };
                //info.CiscoExtention = HpsmCisco.Instance.FindCiscoUser(userName);
                _users.Add(info);
                _raiseUserChanged(info);
                return info;
            }
        }
        public void RegisterUser(WSClientInfo info)
        {
            lock (_lockObject)
            {
                //info.CiscoExtention = HpsmCisco.Instance.FindCiscoUser(info.UserName);
                _users.Add(info);
                _raiseUserChanged(info);
            }
        }
        public void UnregisterUser(WSClientInfo info)
        {
            lock (_lockObject)
            {
                foreach (var user in _users)
                {
                    if (user.UserName == info.UserName && user.WIN == info.WIN)
                    {
                        info.Command = WSClientStatus.closed;
                        _users.Remove(info);
                        _raiseUserChanged(info);
                        return;
                    }
                }
            }
        }

        private void _sendJson(HttpListenerContext context, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = data.Length;
            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
            //context.Response.KeepAlive = true;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
        }

        private bool Process(HttpListenerContext context)
        {
            string rowUrl = context.Request.RawUrl;
            if (rowUrl == "/_stop_server_from_admin_")
            {
                _sendJson(context, "just something for stop");
                return true;
            }
            if (context.Request.HttpMethod == "POST")
            {
                CobaClient client = _createClient();
                if(rowUrl.IndexOf("/print?") >= 0)
                {
                    client.ExecutePrint(context);
                    return false;
                }
                client.ExecutePost(context);
                return false;
            }
            //	Console.WriteLine ("client : " + context.Request.RemoteEndPoint.ToString ());
            string command = context.Request.Url.AbsolutePath;
            if (command.Equals("/command"))
            {
                int i = rowUrl.IndexOf('?');
                if (i > 0)
                {
                    string querystring = rowUrl.Substring(i + 1);
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(querystring);

                    string user = qscoll["user"];
                    string msg = qscoll["text"];
                    this.SetHTTPMessage(user, msg);
                    _sendJson(context, "{\"result\":1,\"data\":0}");
                }
                return false;
            }
            /*
                  if (command.Equals ("/mkdir")) {
                      CobaClient client =  _createClient ();
                      client.CreateFolder (context);
              return false;
            }
                  if (command.Length >= 2 && command[0] == '/' && command[1] == '~') {
                      CobaClient client =  _createClient ();
                      client.Send(context, command);
              return false;
            }*/
            string url = context.Request.Url.ToString();
            //		Console.WriteLine(filename + " url:" + url);
            string filename = command.Substring(1);
            /*
                  if (filename.IndexOf (".php") != -1) {
                      PHP php = new PHP ();
                      php.Execute (context, filename);
                      return;
                  }
            */
            if (string.IsNullOrEmpty(filename))
            {
                foreach (string indexFile in _indexFiles)
                {
                    if (File.Exists(Path.Combine(_webAppDirectory, indexFile)))
                    {
                        filename = indexFile;
                        break;
                    }
                }
            }
            if (rowUrl.IndexOf("/data/") == 0)
            {
                filename = AppDomain.CurrentDomain.BaseDirectory + rowUrl;
            }
            else
            {
                filename = Path.Combine(_webAppDirectory, filename);
            }

            if (File.Exists(filename))
            {
                //Console.WriteLine ("File :" + filename);
                try
                {
                    Stream input = new FileStream(filename, FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read);

                    //Adding permanent http response headers
                    //string mime;
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    //context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentType = Utils.FileNameToMime(filename);
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                    byte[] buffer = new byte[1024 * 64];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        context.Response.OutputStream.Write(buffer, 0, nbytes);
                    input.Close();
                    context.Response.OutputStream.Flush();

                }
                catch (Exception ex)
                {
                    _exception = ex;
                    //Console.WriteLine ("exception: " + ex.ToString ());
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            context.Response.OutputStream.Close();
            return false;
        }

        private void Initialize(string path, int port)
        {
            _webAppDirectory = path;
            _port = port;
            //_serverThread = new Thread(this.Listen);
            //_serverThread.Start();

            Listen();
            _isWorking = true;
            RaiseStarted();
        }

        public static Byte[] EncodeWSMessage(String message)
        {
            Byte[] response;
            Byte[] bytesRaw = Encoding.UTF8.GetBytes(message);
            Byte[] frame = new Byte[10];

            Int32 indexStartRawData = -1;
            Int32 length = bytesRaw.Length;

            frame[0] = (Byte)129;
            if (length <= 125)
            {
                frame[1] = (Byte)length;
                indexStartRawData = 2;
            }
            else if (length >= 126 && length <= 65535)
            {
                frame[1] = (Byte)126;
                frame[2] = (Byte)((length >> 8) & 255);
                frame[3] = (Byte)(length & 255);
                indexStartRawData = 4;
            }
            else
            {
                frame[1] = (Byte)127;
                frame[2] = (Byte)((length >> 56) & 255);
                frame[3] = (Byte)((length >> 48) & 255);
                frame[4] = (Byte)((length >> 40) & 255);
                frame[5] = (Byte)((length >> 32) & 255);
                frame[6] = (Byte)((length >> 24) & 255);
                frame[7] = (Byte)((length >> 16) & 255);
                frame[8] = (Byte)((length >> 8) & 255);
                frame[9] = (Byte)(length & 255);

                indexStartRawData = 10;
            }

            response = new Byte[indexStartRawData + length];

            Int32 i, reponseIdx = 0;

            //Add the frame bytes to the reponse
            for (i = 0; i < indexStartRawData; i++)
            {
                response[reponseIdx] = frame[i];
                reponseIdx++;
            }

            //Add the data bytes to the response
            for (i = 0; i < length; i++)
            {
                response[reponseIdx] = bytesRaw[i];
                reponseIdx++;
            }

            return response;
        }

    }//end
}

