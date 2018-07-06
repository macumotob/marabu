using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;

using System.Net.WebSockets;
using System.Threading;
using System.Net.Sockets;

namespace marabu
{
  public class Requestor
  {
    protected RequestResult result = new RequestResult();

    private const string ERROR_CONNECT_TO_SERVER = "Unable to connect to the remote server";

    private HttpWebRequest _createRequest(string url, string method, string authHeader)
    {
      result.Clear();
      HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
      req.Timeout = 5000;
      if (method != null)
      {
        req.Method = method;
      }
      req.UserAgent = "Mozilla / 5.0(Windows NT 6.3; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2490.80 Safari / 537.36";
      //req.ContentType = "application/xml";
      req.Headers.Add(authHeader);
      req.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
      {
        return true;
      };
      return req;
    }
    private HttpWebResponse _createResponse(HttpWebRequest req)
    {
      try
      {
        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
        return resp;
      }
      catch (Exception ex)
      {
        result.ErrorConnect2RemoteServer = true;
        result.response = ex.Message;
        result.statusDescriptin = ex.Message;
      }
      finally
      {
      }
      return null;
    }

    public RequestResult Send(string url, string method, string authHeader, string data)
    {
      try
      {
        HttpWebRequest req = _createRequest(url, method, authHeader);
        req.ContentType = "application/xml";
        byte[] formData = UTF8Encoding.UTF8.GetBytes(data);
        req.ContentLength = formData.Length;


        using (Stream post = req.GetRequestStream())
        {
          post.Write(formData, 0, formData.Length);
        }

        using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
        {
          StreamReader reader = new StreamReader(resp.GetResponseStream());
          result.response = reader.ReadToEnd();
          result.status = resp.StatusCode;
          result.statusDescriptin = resp.StatusDescription;
        }

      }
      catch (Exception ex)
      {
        result.ErrorConnect2RemoteServer = (ex.Message == ERROR_CONNECT_TO_SERVER);
        result.exception = ex;
      }
      result.CheckResponseState();
      return result;
    }

    public RequestResult Get(string url, string authHeader)
    {
      //RequestResult result = new RequestResult();
      try
      {
        /*
        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        req.Timeout = 5000;
        req.Headers.Add(authHeader);
        req.UserAgent = "Mozilla / 5.0(Windows NT 6.3; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2490.80 Safari / 537.36";
        req.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
        {
          return true;
        };
        */
        HttpWebRequest req = _createRequest(url, null, authHeader);
        HttpWebResponse resp = _createResponse(req);
        if (result.ErrorConnect2RemoteServer)
        {
          return result;
        }
       //        using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        result.response = reader.ReadToEnd();
        result.statusDescriptin = resp.StatusDescription;
        resp.Dispose();

      }
      catch (Exception ex)
      {
        result.ErrorConnect2RemoteServer = (ex.Message == ERROR_CONNECT_TO_SERVER);
        result.exception = ex;
      }
      finally
      {

      }
      return result;
    }

    //public static object XmlDeserializeFromString(this string objectData, Type type)
    //{
    //  var serializer = new System.Xml.Serialization.XmlSerializer(type);
    //  object result;

    //  using (TextReader reader = new StringReader(objectData))
    //  {
    //    result = serializer.Deserialize(reader);
    //  }

    //  return result;
    //}

    
    
    /*
public static RequestResult Get(string url, string authHeader)
{
  RequestResult result = new RequestResult();
  try
  {
    HttpWebRequest req = Requestor.Create(url, "GET", authHeader);
    req.ContentType = null;

    using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
    {
      StreamReader reader = new StreamReader(resp.GetResponseStream());
      result.response = reader.ReadToEnd();
      result.status = resp.StatusCode;
      result.statusDescriptin = resp.StatusDescription;
    }
  }
  catch (Exception ex)
  {
    result.exception = ex;
  }
  result.CheckResponseState();
  return result;
}*/

    /*
    public delegate void EventCiscoGetCompleted(RequestResult result);
    public static async void GetAsync(string url, string authHeader, EventCiscoGetCompleted oncomlete)
    {
      RequestResult result = new RequestResult();
      try
      {
        HttpWebRequest req = Requestor.Create(url, "GET", authHeader);
        using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
        {
          StreamReader reader = new StreamReader(resp.GetResponseStream());
          //result.response = reader.ReadToEnd();
          result.response = await reader.ReadToEndAsync();
          result.statusDescriptin = resp.StatusDescription;
        }
      }
      catch (Exception ex)
      {
        result.ErrorConnect2RemoteServer = (ex.Message == ERROR_CONNECT_TO_SERVER);
        result.exception = ex;
      }
      finally
      {
        if(oncomlete != null)
        {
          oncomlete(result);
        }
        else
        {

        }
      }
      
    }
    */
  }//end of clas
}
