using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace HPTAPIServer
{
  
  class HPWSServer : Server
  {
    public event EventWSUserStateChanged OnWSUserStateChanged;

    public static HPWSServer Instance = new HPWSServer();
    public override string Title
    {
      get
      {
        return "HP_WS_SERVER";
      }
    }

    private List<WSClientInfo> _users = new List<WSClientInfo>();
    /*
    public void Run(string host,int port)
    {
      IPAddress ipaddress = IPAddress.Parse(host);
      _port = port;
      this.Run(ipaddress);
    }*/
    private bool _waitSocketData(NetworkStream stream, int waitTime)
    {
      Stopwatch stopWatch = new Stopwatch();
      stopWatch.Start();
      while (_isWorking && !stream.DataAvailable )
      {
        Thread.Sleep(_sleepTime);
        if(stopWatch.ElapsedMilliseconds  > waitTime)
        {
          stopWatch.Stop();
          return false;
        }
      }
      stopWatch.Stop();
      return _isWorking;
    }
    private void _raiseUserChanged(WSClientInfo info)
    {
      if(this.OnWSUserStateChanged != null)
      {
        this.OnWSUserStateChanged(info);
      }
    }
    private WSClientInfo _findUser(string userName, string win)
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

    public void SetCanReceiveMessage(string userName, string win, bool can)
    {
      lock (_lockObject)
      {
        WSClientInfo user = _findUser(userName, win);
        if (user != null)
        {
          user.CanReceiveMessage = can;
        }
      }
    }
    private WSClientInfo _registerUser(string userName, string win)
    {
      lock (_lockObject)
      {
        WSClientInfo info = new WSClientInfo() { UserName = userName, WIN = win, Command = "reg", Pulse = 1 };
        _users.Add(info);
        _raiseUserChanged(info);
        return info;
      }
    }
    private void _processUser(string command,string userName,string win)
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
        if(command == "tapi")
        {
          command = "tapi-reg";
        }
        WSClientInfo info = new WSClientInfo() { UserName = userName, WIN = win ,Command = command, Pulse=1};
        _users.Add(info);
        _raiseUserChanged(info);
      }
    }
    private  List<byte> _readSocketData(NetworkStream stream)
    {
      List<byte> requestList = new List<byte>();
        while (stream.DataAvailable)
        {
          requestList.Add((byte)stream.ReadByte());
        }
      return requestList;
    }
    private bool _sendHandshake(NetworkStream stream,List<byte> requestList)
    {
      byte[] response = _generateHandshake(requestList.ToArray());
      stream.Write(response, 0, response.Length);
      requestList.Clear();
      return true;
    }
    private void _sendMessage(NetworkStream stream, string message)
    {
      byte[] msg = EncodeMessage(message);
      stream.Write(msg, 0, msg.Length);
    }
    private void _sendClose(NetworkStream stream)
    {
        stream.Write(new byte[4] {136,0,00,00}, 0,4);
    }
    public override void ResponseToClient(TcpClient client)
    {
      string user = "?";
      string win = "0";

      using (var stream = client.GetStream())
      {
        int waitTime = 1000 * 60;
        if (_waitSocketData(stream, waitTime))
        {
          List<byte> requestList = _readSocketData(stream);
          _sendHandshake(stream, requestList);

          while (true)
          {
            if (!_waitSocketData(stream, waitTime))
            {
              break;
            }
            requestList = _readSocketData(stream);

            byte[] msg = requestList.ToArray();

            string text = _decodeMessage(msg);
            stream.Flush();
            //if(text == "tapi")
            //{
            //  string message = Manager.Instance.HP_TAPI_Message;
            //  _sendMessage(stream,message);
            //  Manager.Instance.HP_TAPI_Message = null;
            //}
            //else
            //{
            string[] data = text.Split(';');
            if (data.Length == 2)
            {
              string[] n1 = data[0].Split(':');
              string command = n1[0];
              user = n1[1];
              string[] n2 = data[1].Split(':');
              win = n2[1];
              WSClientInfo info = _findUser(user, win);
              if (info == null)
              {
                info = _registerUser(user, win);
              }
              if (command == "tapi")
              {
                _processUser(command, user, win);
                string message = info.CanReceiveMessage ? info.Message : Manager.Instance.HP_TAPI_Default_Message;
                _sendMessage(stream, message);
                info.Message = Manager.Instance.HP_TAPI_Default_Message;
               // Manager.Instance.HP_TAPI_Message = null;
              }
              else if (command == "reg")
              {
                this.RaiseEvent(text);
                _processUser(command, user, win);
                string message = "{\"result\":\"reg\",\"data\":\"empty\"}";
                _sendMessage(stream, message);
              }
              else if (command == "unreg")
              {
                this.RaiseEvent(text);
                _processUser(command, user, win);
                string message = "{\"result\":\"unreg\",\"data\":\"empty\"}";
                _sendMessage(stream, message);
                break;
              }
              else
              {
                this.RaiseEvent(text);
                _processUser(command, user, win);
              }
            }
            else
            {
              string message = Manager.Instance.HP_TAPI_Message;
              _sendMessage(stream, message);
            }
          }
          
          //}
        }

        _processUser("closed", user, win);
        if(!_isWorking)
        {
          _sendClose(stream);
        }
        client.Close();
       // this.RaiseEvent("WS CLIENT stopped.");
      }
    }

    public void ResponseToClient2(TcpClient client)
    {
      using (var stream = client.GetStream())
      {
        Thread.Sleep(_sleepTime);

        List<byte> requestList = new List<byte>();
        while (!stream.DataAvailable) {
          Thread.Sleep(_sleepTime);
        }

        
        while (stream.DataAvailable)
        {
          requestList.Add((byte)stream.ReadByte());
        }

        byte[] response = _generateHandshake(requestList.ToArray());
        stream.Write(response, 0, response.Length);
        requestList.Clear();

        //while (_isWorking)
        if (_isWorking)
        {
          Thread.Sleep(_sleepTime);
          while (_isWorking &&!stream.DataAvailable) 
          {
            Thread.Sleep(_sleepTime);
          }

          while (stream.DataAvailable)
          {
            requestList.Add((byte)stream.ReadByte());
          }
          if (_isWorking)
          {
            byte[] msg = requestList.ToArray();

            string text = _decodeMessage(msg);
            string message = Manager.Instance.HP_TAPI_Message;
            msg = EncodeMessage(message);
            stream.Write(msg, 0, msg.Length);
            Manager.Instance.HP_TAPI_Message = null;
          }
        }
        client.Close();
      }
    }
    private byte[] _generateHandshake(byte[] request)
    {
      string requestString = Encoding.UTF8.GetString(request);

      Dictionary<string, string> headers = new Dictionary<string, string>();
      string[] lines = requestString.Split('\n');

      foreach (string line in lines)
      {
        string[] tokens = line.Split(new char[] { ':' }, 2);
        if (!string.IsNullOrWhiteSpace(line) && tokens.Length > 1)
        {
          headers[tokens[0]] = tokens[1].Trim();
        }
      }

      string responseKey = "";
      string key = string.Concat(headers["Sec-WebSocket-Key"], "258EAFA5-E914-47DA-95CA-C5AB0DC85B11");
      using (SHA1 sha1 = SHA1.Create())
      {
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(key));
        responseKey = Convert.ToBase64String(hash);
      }
      StringBuilder sb = new StringBuilder();

      sb.Append("HTTP/1.1 101 Switching Protocols\r\n");
      sb.Append("Upgrade: websocket\r\n");
      sb.Append("Connection: Upgrade\r\n");
      sb.AppendFormat("Sec-WebSocket-Accept: {0}\r\n", responseKey);
      //sb.Append("Sec-WebSocket-Protocol: chat");
      sb.Append("\r\n");
      Console.WriteLine("Clent key :" + responseKey);
      return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public static string _decodeMessage2(byte[] bytes)
    {
      string text = "";
      List<byte[]> ret = new List<byte[]>();
      int offset = 0;
      while (offset + 6 < bytes.Length)
      {
        // format: 0==ascii/binary 1=length-0x80, byte 2,3,4,5=key, 6+len=message, repeat with offset for next...
        int len = bytes[offset + 1] - 0x80;
        //String data = Encoding.UTF8.GetString(bytes);
        //Debug.Log("len=" + len + "bytes[" + bytes.Length + "]=" + ByteArrayToString(bytes) + " data[" + data.Length + "]=" + data);
        //Debug.Log("len=" + len + " offset=" + offset);
        byte[] key = new byte[] { bytes[offset + 2], bytes[offset + 3], bytes[offset + 4], bytes[offset + 5] };
        byte[] decoded = new byte[len];
        for (int i = 0; i < len; i++)
        {
          int realPos = offset + 6 + i;
          decoded[i] = (byte)(bytes[realPos] ^ key[i % 4]);
        }
        offset += 6 + len;
        string s = System.Text.Encoding.UTF8.GetString(decoded);
        text += s;
        ret.Add(decoded);
      }
      return text;
    }
    private static string _decodeMessage(Byte[] bytes)
    {
      string incomingData = String.Empty;
      Byte secondByte = bytes[1];
      Int32 dataLength = secondByte & 127;
      Int32 indexFirstMask = 2;
      if (dataLength == 126)
        indexFirstMask = 4;
      else if (dataLength == 127)
        indexFirstMask = 10;

      IEnumerable<Byte> keys = bytes.Skip(indexFirstMask).Take(4);
      Int32 indexFirstDataByte = indexFirstMask + 4;

      Byte[] decoded = new Byte[bytes.Length - indexFirstDataByte];
      for (Int32 i = indexFirstDataByte, j = 0; i < bytes.Length; i++, j++)
      {
        decoded[j] = (Byte)(bytes[i] ^ keys.ElementAt(j % 4));
      }
      //string s = Encoding.UTF7.GetString(decoded, 0, decoded.Length);
      //string s1 = Encoding.Unicode.GetString(decoded, 0, decoded.Length);
      incomingData = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
      if(incomingData.Length != dataLength)
      {

      }
      return incomingData;
    }
    private static Byte[] EncodeMessage(String message)
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
  }//class
}
