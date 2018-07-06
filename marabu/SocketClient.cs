using System;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections.Generic;
#if _PARANOIA_
 using System.Diagnostics;
#endif

namespace marabu
{
  using System.Net.NetworkInformation;
  using System.Web.Script.Serialization;
  using coba;

  public class SocketClient
  {

    public delegate void EventSocketExecuteResult(bool result);

    private CobaServer _server;
    private Exception _exception;
    private WSClientInfo _hpsmUser = new WSClientInfo();
    //private CiscoClient _ciscoUser;// = new CiscoClient();
    //private CiscoNotifier _notifier;
    private Logger _logger;
    private Logger _logger_history;
#if CISCO_HISTORY
    private CiscoDialogsHistory _history = new CiscoDialogsHistory();
#endif
    private string _remoteAddress;
    private string _hpsmFolder;
    private string _hpsmField;
    private string _ciscoHost;
    private int _ciscoPort;
    private int _ciscoBindPort;

    private string _cisco_user_state = null;
    private bool _full_log_mode = true;

    JavaScriptSerializer _serializer = new JavaScriptSerializer();


    public SocketClient(CobaServer server,string hpsmFolder,string hpsmField,string ciscoHost,int ciscoPort,int ciscoBindPort ,bool fullLogMode)
    {
      _server = server;
      _hpsmFolder = hpsmFolder;
      _hpsmField = hpsmField;
      _ciscoHost = ciscoHost;
      _ciscoPort = ciscoPort;
      _ciscoBindPort = ciscoBindPort;
      _logger = Manager.SrvLog;
      _full_log_mode = fullLogMode;
    }
    ~SocketClient()
    {
      _serializer = null;
      _server = null;
      _logger_history = null;
      _logger = null;
    }
    public async void Execute(HttpListenerContext listenerContext)
    {
      _exception = null;
      //int count = 0;
      _remoteAddress = listenerContext.Request.RemoteEndPoint.ToString();
      WebSocketContext webSocketContext = null;
      try
      {
        // When calling `AcceptWebSocketAsync` the negotiated subprotocol must be specified. This sample assumes that no subprotocol 
        // was requested. 
        webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: null);
        //Interlocked.Increment(ref count);
      }
      catch (Exception e)
      {
        _exception = e;
        listenerContext.Response.StatusCode = 500;
        listenerContext.Response.Close();
        return;
      }

      WebSocket webSocket = webSocketContext.WebSocket;
      if(webSocket.State != WebSocketState.Open)
      {

      }
      try
      {
        byte[] buffer = new byte[1024];
        
        
        while ( _exception == null && webSocket.State == WebSocketState.Open)
        {
          WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
          if ((_hpsmUser.Command == WSClientStatus.closed || _hpsmUser.Command == WSClientStatus.unreg))
          {
            break;
          }
          if (receiveResult.MessageType == WebSocketMessageType.Close)
          {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
          }
          else if (receiveResult.MessageType == WebSocketMessageType.Text)
          {
            string command = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
            _response_on_command(webSocket, command);
            // await webSocket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "Cannot accept text frame", CancellationToken.None);
          }
          else //binary data
          {
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, receiveResult.Count), WebSocketMessageType.Binary, receiveResult.EndOfMessage, CancellationToken.None);
          }
          //if(_exception != null)
          //{
          //  break;
          //}
        }//end if client loop
      }
      catch(ApplicationException e)
      {
        _exception = e;
      }
      catch (Exception e)
      {
        _exception = e;
      }
      finally
      {
        _unregister_marabu_user();
      }
      //if (webSocket != null)
      //{
      //  webSocket = null;
      //}
    }

    private PRNClientCommand _parser_command(string cmd)
    {
      try
      {
        var query = _serializer.Deserialize<PRNClientCommand>(cmd);
        return query;
      }
      catch (Exception ex)
      {
        _exception = ex;
        return null;
      }
    }
    private async void _send_command(WebSocket webSocket, PRNClientCommand query)
    {
#if DEBUG
      if(query.status == null)
      {

      }
#endif
      
      try
      {
        string message = _serializer.Serialize(query);
        if (webSocket.State == WebSocketState.Aborted)
        {
          return;
        }
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        //ww await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
      }
      catch(WebSocketException ex)
      {
        _exception = ex;
        _logger.Log(ex, "WebSocketException. Socket Error Code {0} Native Code {1} ",    ex.WebSocketErrorCode, ex.NativeErrorCode);
      }
      catch (Exception ex)
      {
        _exception = ex;
        _logger.Log(ex, "Сервер MARABU остановлен при работающих операторах.");
      }
    }
//    private async void _send_command2(WebSocket webSocket, HPSMClientCommand query)
//    {
//#if DEBUG
//      if (query.status == null)
//      {

//      }
//#endif
//      CancellationTokenSource source = new CancellationTokenSource();
//      CancellationToken token = source.Token;
      
//      try
//      {

//        string message = _serializer.Serialize(query);
//        if (webSocket.State == WebSocketState.Aborted)
//        {
//          return;
//        }
//        byte[] buffer = Encoding.UTF8.GetBytes(message);
//        //ww await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
//        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, token);
//      }
//      catch (WebSocketException ex)
//      {
//        _exception = ex;
//        _logger.Log(ex, "WebSocketException. Socket Error Code {0} Native Code {1} ", ex.WebSocketErrorCode, ex.NativeErrorCode);
//      }
//      catch (Exception ex)
//      {
//        _exception = ex;
//        _logger.Log(ex, "Общее исключение.");
//      }
//    }
    //private void _send_cisco_error(WebSocket webSocket, HPSMClientCommand query,string error)
    //{
    //  query.cmd = text.CISCO_COMMAND_ERROR;
    //  query.status = error;
    //  _send_command(webSocket, query);
    //}
    private void _send_heart_bit(WebSocket webSocket, PRNClientCommand query)
    {
      query.cmd = text.STATE_TAPI;
      query.status = text.STATE_TAPI;
      _send_command(webSocket, query);
    }
    //private bool _isNotifierAlive(WebSocket webSocket, HPSMClientCommand query)
    //{
    //  CiscoMessage m = _notifier.GetMessage();
    //  if (m == null)
    //  {
    //    return true;
    //  }
    //  if(m.type == CiscoMessageType.error)
    //  {
    //    _sendCiscoError(webSocket, query,m.message);
    //    return false;
    //  }
    //  return true;
    //}
    private void _register_hpsm_user(WebSocket webSocket, PRNClientCommand query, string cmd)
    {
      _hpsmUser.Command = WSClientStatus.reg;

      _hpsmUser.UserName = query.user;
      _hpsmUser.WIN = query.wnd;
      _hpsmUser.RemoteAddress = _remoteAddress;

      _server.RegisterUser(_hpsmUser);
      _hpsmUser.CiscoExtention = _hpsmUser.CiscoExtention == null ? "none" : _hpsmUser.CiscoExtention;

      _logger = null;
      _logger = new Logger(_hpsmUser.CiscoExtention, Manager.SrvLog.Folder);
      _logger.Log("Пользователь {0} начал работу.", _hpsmUser.CiscoExtention);

      _logger_history = new Logger(_hpsmUser.CiscoExtention + "H", Manager.SrvLog.Folder, false);
      //if (_ciscoUser == null)
      //{
      //  _ciscoUser = new CiscoClient(_ciscoHost, _ciscoPort);
      //  _ciscoUser.FullLogMode = _full_log_mode;
      //  _ciscoUser.Log = _logger;
      //}

      //_ciscoUser.ResetExtemtion(_hpsmUser.CiscoExtention);

#if CISCO_HISTORY
      //_history.LoadHistory(_hpsmUser.CiscoExtention);
      _history.SetExtention(_hpsmUser.CiscoExtention);
#endif
      //query.ciscoext = _hpsmUser.CiscoExtention;
      query.folder = _hpsmFolder;
      query.field = _hpsmField;
      query.status = _cisco_user_state == null ? text.HPSM_REGISTER_USER : _cisco_user_state;
      _send_command(webSocket, query);

      //if (_notifier == null)
      //{
      //  //_notifierFatal = false;
      //  _notifier = new CiscoNotifier(_ciscoHost, _ciscoBindPort, _hpsmUser.CiscoExtention, _hpsmUser.CiscoExtention);
      //  _notifier.FullLogMode = _full_log_mode;
      //  _notifier.Log = _logger;
      //  _notifier.Listen(); 
      //}
      //_ciscoUser.Listen();
    }
    private void _unregister_marabu_user()
    {
      //Debug.Print("Marabu client unregister....");
      //if (_notifier != null)
      //{
      //  _notifier.Terminate();
      //  _notifier.WaitTernination();
      //  _notifier = null;
      //}
      //if (_ciscoUser != null)
      //{
      //  _ciscoUser.Terminate();
      //  _ciscoUser.WaitTernination();
      //  _ciscoUser = null;
      //}
      _logger.Log("Пользователь {0} закончил работу.", ( _hpsmUser != null &&  _hpsmUser.CiscoExtention != null ? _hpsmUser.CiscoExtention : "NONE"));
      _server.UnregisterUser(_hpsmUser);
      //Debug.Print("Marabu client unregistered.");
    }
    //private bool _notifierFatal;

    private void _unregister_hpsm_user(WebSocket webSocket, PRNClientCommand query, string cmd)
    {
      _hpsmUser.Command = WSClientStatus.unreg;
      _send_command(webSocket, query);
      _unregister_marabu_user();
    }
    private void _response_to_hpsm_user(WebSocket webSocket, PRNClientCommand query)
    {
      //CiscoMessage m = _notifier.GetMessage();
      //if (m != null)
      {
                query.cmd = "aaaaaaaaaa";// m.type.ToString();
        //if (m.type == CiscoMessageType.error)
        //{
        //  if(!_notifier._isAlive)
        //  {

        //  }
        //}
        //query.dialog = m.message;
        //query.status = m.message;
        _send_command(webSocket, query);
        return;
      }
      //m = _ciscoUser.GetMessage();
      //if (m != null)
      //{
      //  query.cmd = m.type.ToString();
      //  query.dialog = m.message;
      //  query.status = m.message;
      //  _send_command(webSocket, query);
      //  if (m.type == CiscoMessageType.error)
      //  {
      //    if (m.message == "FATAL")
      //    {

      //    }
      //  }
      //  return;
      //}

      _send_heart_bit(webSocket,  query);
      
      //_hpsmUser.Command = WSClientStatus.tapi;
      //query.status = text.STATE_TAPI;
      //_sendCommand(webSocket, query);
    }
    private void _cisco_login(WebSocket webSocket, PRNClientCommand query)
    {
      //if(_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_LOGIN;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);
    }
    private void _cisco_logout(WebSocket webSocket, PRNClientCommand query)
    {
      //if (_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_LOGOUT;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);
    }
    
    private void _cisco_dialog_action(WebSocket webSocket, PRNClientCommand query)
    {
      //if (_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_DIALOG_ACTION;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);

    }
    private void _cisco_dialog_consult_call(WebSocket webSocket, PRNClientCommand query)
    {
      //if (_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_CONSULTANT_CALL;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);

    }
    private void _cisco_change_user_status(WebSocket webSocket, PRNClientCommand query)
    {
      //if (_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_USER_STATE_CHANGE;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);
    }
    private void _cisco_make_call(WebSocket webSocket, PRNClientCommand query)
    {
      //if (_notifier.State == CiscoClientState.Running)
      //{
      //  query.cmd = text.FINESSE_MAKE_CALL;
      //  _ciscoUser.PushQuery(query);
      //}
      //else
      //{
      //  _notifier.Reset();
      //}
      _send_heart_bit(webSocket, query);

    }

    private void _cisco_dialog_history(WebSocket webSocket, PRNClientCommand query)
    {
     // string data = query.dialog;
     // if (!string.IsNullOrEmpty(data))
     // {
     ////   data = data.Replace('\t', (char)1);
     //   _logger_history.Log(data);
     // }
     // _send_heart_bit(webSocket, query);
    }

    private void _response_on_command(WebSocket webSocket, string cmd)
    {
      Thread.Sleep(10);
      var query = _parser_command(cmd);
      if (query == null)
      {
        return;
      }
      switch (query.cmd)
      {
        case text.HPSM_HEART_BIT          : _response_to_hpsm_user(webSocket, query); break;
        case text.HPSM_REGISTER_USER      : _register_hpsm_user(webSocket, query, cmd); break;
        case text.HPSM_UNREGISTER_USER    : _unregister_hpsm_user(webSocket, query, cmd); break;
        case text.HPSM_CHANGE_USER_STATE  : _cisco_change_user_status(webSocket, query); break;
        case text.HPSM_MAKE_CALL          : _cisco_make_call(webSocket,query); break;
        case text.FINESSE_LOGIN           : _cisco_login(webSocket,query); break;
        case text.FINESSE_LOGOUT          : _cisco_logout(webSocket, query); break;
        case text.HPSM_DIALOG_ACTION      : _cisco_dialog_action(webSocket, query); break;
        case text.HPSM_REDIRECT           : _cisco_dialog_consult_call(webSocket, query); break;
        case text.HPSM_HISTORY            : _cisco_dialog_history(webSocket, query);  break;

        default:
          Thread.Sleep(100);
          //_server.RaiseEvent(cmd);
          //_server.ProcessUser(WSClientStatus.unknown, query.user, query.wnd);
          break;
      }

    }
  }//end of class
}
// END OF FILE