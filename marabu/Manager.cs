using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  using System.Configuration;
  using coba;
  using System.Windows.Forms;
  public delegate void EventWSUserStateChanged(WSClientInfo info);
  class Manager
  {

    public delegate void EventManagerStopped();
    public delegate void EventManagerStarted();

    public event EventManagerStopped OnStopped;
    public event EventManagerStarted OnStarted;
    public static Manager Instance = new Manager();

    public ServerSettings Settings = new ServerSettings();

    public static Logger Log = new Logger("mrb", null);
    public static Logger SrvLog;
    public event EventMessageOnServer OnMessage;

    private object _lockObject = new object();


    //public static string UsersFileName
    //{
    //  get
    //  {
    //    return AppDomain.CurrentDomain.BaseDirectory + "users.txt";
    //  }
    //}

    public static string MessageToString(string result = "0", string data = "empty", string p = "[]")
    {

      return "{" +
        string.Format("\"result\":\"{0}\",\"data\":\"{1}\",\"p\":{2}", result, data, p)
        + "}";
    }
    private string _message;
    public string HP_TAPI_Message
    {
      get
      {
        lock (_lockObject)
        {
          if (_message == null)
          {
            _message = MessageToString();
          }
          return _message;
        }
      }
      set
      {
        lock (_lockObject)
        {
          if (value == null)
          {
            _message = MessageToString();
          }
          else
          {
            _message = value;
          }
          _wsServer.SetMessage(_message);
        }
      }
    }

    //public void SetTAPIMessage(string text, List<TAPIParameter> paramsList)
    //{
    //  string s = "[";

    //  for (int i = 0; i < paramsList.Count; i++)
    //  {
    //    if (i > 0) s += ",";
    //    s += paramsList[i].ToString();
    //  }
    //  s += "]";
    //  text = text.Replace("/", "%2F").Replace(" ", "%20");
    //  string msg = MessageToString("ok", text, s); //"{\"result\":\"ok\",\"data\":\"" + text +"\",\"p\":" + s + ",\"ui\":" +userUImode.ToString() + "}";
    //  HP_TAPI_Message = msg;
    //}

    public static string MakeHTTPMessage(string menuItemId, string fieldName,string value)
    {
            //List<TAPIParameter> paramsList = new List<TAPIParameter>();
            //TAPIParameter p = new TAPIParameter() { Id = fieldName, Value = value };
            //paramsList.Add(p);
            //string s = "[";

            //for (int i = 0; i < paramsList.Count; i++)
            //{
            //  if (i > 0) s += ",";
            //  s += paramsList[i].ToString();
            //}
            //s += "]";
            //menuItemId = menuItemId.Replace("/", "%2F").Replace(" ", "%20");
            //string msg = MessageToString("ok", menuItemId, s); //"{\"result\":\"ok\",\"data\":\"" + text +"\",\"p\":" + s + ",\"ui\":" +userUImode.ToString() + "}";
            //return msg;
            return null;
    }

    private CobaServer _wsServer = new CobaServer();
    public CobaServer WSServer
    {
      get
      {
        return _wsServer;
      }
    }

    public void InitHandlers()
    {
      _wsServer.OnMessage += OnMessage;
      _wsServer.OnStopped += _OnServerStopped;
      _wsServer.OnStarted += _OnServerStarted;
    }
    public void Start()
    {
      Settings.LoadConfig();
      Manager.SrvLog = Manager.Log;
      
      Manager.SrvLog =  new Logger("srv", Settings.ServerLogFolder);
      //HpsmCisco.Instance.Load();

      _text = "";
      _wsServer.Run(Settings);

    }

    private string _text;
    public string StopMessage
    {
      get
      {
        return _text;
      }
    }
    void _OnServerStarted(Server server)
    {
      _text = "";
      Manager.Log.Log("Server {0} started.",server.ToString());
      Manager.SrvLog.Log("SERVER STARTED.");
      OnStarted();
    }
    void _OnServerStopped(Server server)
    {
      _text += server.ToString() + " stopped." + Environment.NewLine;
      Manager.Log.Log("Server {0} stoped.", server.ToString());
      Manager.SrvLog.Log("SERVER STOPPED.");
      OnStopped();
    }
    public void Stop()
    {
      _wsServer.Stop();

    }
    void _httpServer_OnMessageFromClient(string text)
    {
      this.HP_TAPI_Message = text;
    }

    //public List<TAPICommand> LoadCommands()
    //{
    //  string _fileName = AppDomain.CurrentDomain.BaseDirectory + "tapidata.json";
    //  var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

    //  if (System.IO.File.Exists(_fileName))
    //  {
    //    string data = System.IO.File.ReadAllText(_fileName, Encoding.UTF8);
    //    List<TAPICommand> list = (List<TAPICommand>)serializer.Deserialize(data, typeof(List<TAPICommand>));
    //    return list;
    //  }
    //  return null;
    //}

    //public string LogFileName
    //{
    //  get
    //  {
    //    return AppDomain.CurrentDomain.BaseDirectory + "hptapi.log";
    //  }
    //}
    //public string GetLogFileContent()
    //{
    //  lock (_lockObject)
    //  {
    //    if (System.IO.File.Exists(LogFileName))
    //    {
    //      string s = System.IO.File.ReadAllText(LogFileName, Encoding.UTF8);
    //      return s;
    //    }
    //    return "Log File was deleted by admin";
    //  }
    //}
    //public void ClearLogFile()
    //{
    //  lock (_lockObject)
    //  {
    //    System.IO.File.Delete(LogFileName);
    //    Log("Log File was deleted by admin");
    //  }
    //}
    //public void Log(string logMessage)
    //{
    //  lock (_lockObject)
    //  {
    //    try {
    //      using (System.IO.StreamWriter w = System.IO.File.AppendText(LogFileName))
    //      {
    //        w.Write("\r\n{0} {1}:   ", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
    //        w.Write("{0}", logMessage);
    //      }
    //    }
    //    catch (Exception ex)
    //    {
    //    //  OnMessage(e.ToString());
    //    }
    //  }
    //}
#region WEB UI
    public void launchWebUI()
    {
      string url = _wsServer.ToStringURL();
     System.Diagnostics.Process.Start(url);

      // open in Internet Explorer
//      Process.Start("iexplore", @"http://www.stackoverflow.net/");

      // open in Firefox
//      Process.Start("firefox", @"http://www.stackoverflow.net/");

      // open in Google Chrome
//      Process.Start("chrome", @"http://www.stackoverflow.net/");
    }
#endregion
  }//end of class
}
