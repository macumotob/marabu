using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{

  public enum WSClientStatus
  {
    reg,
    tapi,
    tapi_reg,
    unreg,
    closed,
    unknown
  }
  public class WSClientInfo
  {
    public string UserName { get; set; }
    public string CiscoExtention { get; set; }
    public string WIN { get; set; }
    public string Message { get; set; }
    public string RemoteAddress { get; set; }
    public long Pulse { get; set; }
    public bool CanReceiveMessage { get; set; }
    public bool isUnregistered { get; set; }
    public WSClientStatus Command { get; set; }
    public string CommandFromHTTP { get; set; }

    public string CallStatus { get; set; }

  }
}
