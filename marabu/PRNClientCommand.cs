using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  internal class PRNClientCommand
  {
    public string cmd { get; set; }
    public string user { get; set; }
    public string wnd { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    //public string dialogid { get; set; }
    //public string dialog { get; set; }
    public string folder { get; set; }
    public string field { get; set; }
    //public string ciscoext { get; set; }

    public PRNClientCommand Clone()
    {
      PRNClientCommand c = new PRNClientCommand()
      {
        cmd = this.cmd,
        user = this.user,
        wnd = this.wnd,
        status = this.status,
        address = this.address,
        //dialogid = this.dialogid,
        //dialog = this.dialog,
        folder = this.folder,
        field = this.field,
      //  ciscoext = this.ciscoext
      };
      return c;
    }
  }
}
