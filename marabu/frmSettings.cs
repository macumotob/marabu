using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;

namespace HPTAPIServer
{
  public partial class frmSettings : Form
  {
    public frmSettings()
    {
      InitializeComponent();
      //_cmdSave.Click += delegate (object s, EventArgs e)
      //{
      //  _updateData();
      //};

      //_cmdSave_Click;
    }

    void _cmdSave_Click(object sender, EventArgs e)
    {
      _updateData();
      this.Close();
    }
    protected override void OnShown(EventArgs e)
    {
      _updateControls();
      base.OnShown(e);
    }
    private void _updateControls()
    {
      Manager.ServerSettings s = Manager.Instance.Settings;
      s.LoadConfig();

      List<string> lstIp = _getListIPAddress();

      _cmbHttpHost.DataSource = lstIp;
      _cmbHttpHost.SelectedItem = s.HttpHost;


      _txtHttpPort.Text = s.HttpPort;

    }
    private void _updateData()
    {
      Manager.ServerSettings s = Manager.Instance.Settings;
      s.HttpHost = _cmbHttpHost.Text;
      s.HttpPort = _txtHttpPort.Text;

      Manager.Instance.Settings.Save();
    }

    private List<string> _getListIPAddress()
    {
      IPAddress[] addresses = Dns.GetHostEntry(Environment.MachineName).AddressList;
      List<string> lst_str = new List<string>();
      string s = "";
    
      for (int i = 0; i < addresses.Length; i++)
      {
        IPAddress a = addresses[i];
        s = a.ToString();
        //if (s.Substring(0, 5) != "fe80:")
        if(!s.Contains(":"))
        {
          lst_str.Add(s);
        }
      }
      return lst_str;
    }
  }
}
