using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marabu
{
  public partial class CiscoTest : Form
  {
    //CiscoClient _cisco = new CiscoClient("2003");
    public CiscoTest()
    {
      InitializeComponent();
    }

    private void _cmdSend_Click(object sender, EventArgs e)
    {
      //CiscoClient c = new CiscoClient("2003");
      //c.Login();
      //c.GetUserInfo();
      //return;
      
      //c.ChangeUserStatus("READY");
      //c.MakeCall("2003", "2002");
      ////_txtResult.Text = CiscoClient.ciscoSignIn();
      ////.HttpPut("http://10.0.0.207:8445/finesse/api/User/2002");// _cisco.CiscoGetPolicy(_txtRequest.Text);//.Login2Sandbox();
    }
  }
}
