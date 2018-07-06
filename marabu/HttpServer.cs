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
  class HttpServer :Server
  {
    public override string Title
    {
      get
      {
        return "HP_HTTP_SERVER";
      }
    }
    /*
    public void Run(string host = "192.168.0.46", int port = 13002)
    {
      IPAddress address = IPAddress.Parse(host);  // _findAddress(host, port);
      _host = host;
      _port = port;
      if(address == null)
      {
        this.RaiseEvent("Error resolve host: " + _host + " port: " + _port.ToString());
        return;
      }
      this.Run(address);
    }
      */
    /*
    public override void Stop()
    {
      base.Stop();
   //   _isStopped = _task.Wait(5000);

    }*/
    public override void ResponseToClient(TcpClient client) //  void SendResponse(ref TcpClient client)
    {
      using (var stream = client.GetStream())
      {
        List<byte> requestList = new List<byte>();

        //wait until there is data in the stream
        while (!stream.DataAvailable) { }

        //read everything in the stream
        while (stream.DataAvailable)
        {
          requestList.Add((byte)stream.ReadByte());
        }

        //send response
        byte[] response = GenerateResponse(requestList.ToArray());
        stream.Write(response, 0, response.Length);
      }
    }
    public byte[] GenerateResponse(byte[] request)
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
        else
        {
          int i = line.IndexOf('=');
          if (i >= 0)
          {
            int end = line.IndexOf(" HTTP");
            string s = line.Substring(i + 1, end - i - 1);
            s = s.Replace("%20", " ");
            //HP_TAPI_Message = "{\"result\":\"ok\",\"data\":\"" + s + "\"}";
            this.RaiseMessageFromClient("{\"result\":\"ok\",\"data\":\"" + s + "\"}");
          }
        }
      }
      StringBuilder sb = new StringBuilder();
      sb.Append("HTTP/1.1 200 OK\r\n");
      sb.Append("\r\n");
      return Encoding.UTF8.GetBytes(sb.ToString());
    }


  }//end of class
}
