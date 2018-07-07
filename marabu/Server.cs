using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace marabu
{
  public delegate void EventMessageFromClient(string text);
  public delegate void EventMessageOnServer(string text);
  public delegate void EventServerStopped(Server server);
  public delegate void EventServerStart(Server server);
  public delegate void EventServerClientsCount(Server server , int count);

  public abstract class Server
  {
    public abstract string Title { get; }
    public class ServerState
    {
      public int State {get;set;}
    }
    public event EventMessageOnServer OnMessage;
    public event EventMessageFromClient OnMessageFromClient;
    public event EventServerStopped OnStopped;
    public event EventServerStart OnStarted;
    public event EventServerClientsCount OnClientCount;

    protected CancellationTokenSource _tokenSource;

    protected Task _task;
    protected string _host;
    protected int _port;
    protected bool _is_working = false;
    protected bool _isWorking
    {
      get
      {
        lock (_lockObject)
        {
          return _is_working;
        }
      }
      set
      {
        lock (_lockObject)
        {
          _is_working = value;
        }

      }
    }

    protected bool _isStopped = false;
    protected int _sleepTime = 100;

    protected object _lockObject = new object();

    private int _clcount;
    private int _clientCount
    {
      get
      {
        return _clcount;
      }
      set
      {
        _clcount = value;
        if(OnClientCount != null)
        {
          OnClientCount(this, _clcount);
        }
      }
    }
    private IPAddress _ipAddress;

    public void RaiseStopped()
    {
      if (this.OnStopped != null)
      {
        OnStopped(this);
        return;
      }
      throw new Exception("Event handler not found!");
    }
    public void RaiseStarted()
    {
      if (this.OnStarted != null)
      {
        OnStarted(this);
        return;
      }
      throw new Exception("Event handler not found!");
    }

    //public void RaiseEvent(string text)
    //{
    // // Manager.Instance.Log(text);
    //}
    public void RaiseMessageFromClient(string text)
    {
      if (this.OnMessageFromClient != null)
      {
        OnMessageFromClient(text);
        return;
      }
      throw new Exception("Event handler not found!");
    }

    protected IPAddress _findAddress(string host, int port)
    {
      _host = host;
      _port = port;

      if(host == IPAddress.Loopback.ToString())
      {
        _host = IPAddress.Loopback.ToString();
        return IPAddress.Loopback;
      }
      IPAddress[] addresses = Dns.GetHostEntry(Environment.MachineName).AddressList;
      IPAddress address = null;
      for (int i = 0; i < addresses.Length; i++)
      {
        IPAddress a = addresses[i];
        if (a.ToString() == host)
        {
          _host = host;
          address = a;
          break;
        }
      }
      return address;
    }

    //public virtual bool Run2(string host, int port)//IPAddress address)
    //{
    //  if (_isWorking)
    //  {
    //    //RaiseEvent("Already Running on host: " + _host + " port: " + _port.ToString());
    //    RaiseStopped();
    //    _isWorking = false;
    //    return false;
    //  }
    //  IPAddress address = _findAddress(host, port);//IPAddress.Parse(host);
    //  if (address == null)
    //  {
    //    RaiseStopped();
    //    return false;
    //  }

    //  _ipAddress = address;
    //  _tokenSource = new CancellationTokenSource();
    //  CancellationToken ctoken = _tokenSource.Token;
    //  Server.ServerState x = new Server.ServerState();
    //  x.State = 0;


    //  _task = Task.Factory.StartNew((x2) =>
    //  {
    //    _clientCount = 0;
    //    ctoken.ThrowIfCancellationRequested();
    //    _isWorking = true;

    //    _host = _ipAddress.ToString();
    //    //_port = 8181;

    //    var listener = new TcpListener(_ipAddress, _port);
    //    RaiseStarted();
    //    listener.Start();
        

    //  //  this.RaiseEvent("Http server host :" + _host + " port: " + _port.ToString());
    //    while (_isWorking)
    //    {
    //      Thread.Sleep(_sleepTime);
    //      if (ctoken.IsCancellationRequested)
    //      {
    //        break;
    //      }
    //      var client = listener.AcceptTcpClient();
        
    //      Task.Factory.StartNew((x3) =>
    //      {
    //        _clientCount++;
    //        Thread.Sleep(_sleepTime);
    //        if ( _isWorking)//!ctoken.IsCancellationRequested)
    //        {
    //          ResponseToClient(client);
    //        }
    //      }, x, TaskCreationOptions.LongRunning).ContinueWith( (t) => 
    //      {
    //        _clientCount--;
    //      });
    //    }
    //    listener.Stop();
    //  }, _tokenSource, TaskCreationOptions.LongRunning);
    //  return true;
    //}
    public virtual void ResponseToClient(TcpClient client)
    {

    }
    public virtual void Stop()
    {
      if(!_is_working)
      {
        return;
      }
      _isWorking = false;
      _tokenSource.Cancel();
      TcpClient client = new TcpClient();
      client.Connect(_ipAddress, _port);
 
      try
      {
        _task.Wait(5000);
      }
      catch (AggregateException e)
      {
        foreach (var v in e.InnerExceptions)
          Console.WriteLine(e.Message + " " + v.Message);
      }
      finally
      {
        _tokenSource.Dispose();
        _tokenSource = null;
      }
      this.RaiseStopped();
    }

    //public virtual void Stop2()
    //{
    //  _isWorking = false;
    //  TcpClient client = new TcpClient();
    //  client.Connect(_host, _port);
    //}
    public override string ToString()
    {

      return this.Title + "[" + _host + ":" + _port.ToString() + "]";
    }
  }//
}
