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


        public PRNClientCommand Clone()
        {
            PRNClientCommand c = new PRNClientCommand()
            {
                cmd = this.cmd,
                user = this.user,
                wnd = this.wnd,
                status = this.status,
            };
            return c;
        }
    }
}
