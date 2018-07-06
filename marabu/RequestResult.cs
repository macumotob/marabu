using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Net;
//using System.Text;
//using System.Threading.Tasks;

namespace marabu
{
  public class RequestResult
  {
      public Exception exception;
      public string response;
      public HttpStatusCode status = HttpStatusCode.Unused;
      public string statusDescriptin;
      public bool ErrorConnect2RemoteServer;

    public void Clear()
    {
      exception = null;
      response = null;
      statusDescriptin = null;
      ErrorConnect2RemoteServer = false;
    }
      public string CheckResponseState()
      {
        /*
              switch ((int)result.status)
              {
                case 401: msg = "Parameter Missing"; break;
                case 402: msg = "Invalid Input (a request in a Unified CCX deployment includes mediaProperties)"; break;
                case 403: msg = "Invalid Destination (the toAddress and fromAddress are the same)"; break;
                case 404: msg = "Authorization Failure"; break;
                case 405: msg = "Invalid Authorization"; break;
                case 500: msg = "Internal Server Error"; break;
                default: msg = "Unknown status,ask CISCO gays"; break;
              }
              */
        if (exception != null)
        {
          response = text.CISCO_ERROR_TEXT + "\n" + exception.Message;
        }
        switch ((int)status)
        {
          case 200: response = "Success"; break;
          case 202: response = "Accepted"; break;
          case 400: response = "Bad Request"; break;
          case 401: response = "Invalid Supervisor"; break;
          case 402: response = "Unauthorized"; break;
          case 404: response = "Not Found"; break;
          case 500: response = "Internal Server Error"; break;
          case 503: response = "Service Unavailable"; break;
          default: break;
        }
        return response;
      }
  } //end of class 
}
