using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace marabu
{
  public partial class frmMain : Form
  {
    Manager _manager = Manager.Instance;

    //private List<TAPICommand> _commands;

    bool _isRunning = false;
    public frmMain()
    {
      InitializeComponent();
      _cmbCommand.SelectedIndexChanged += _cmbCommand_SelectedIndexChanged;
      _cmbCiscoHosts.SelectedIndexChanged += _cmbCiscoHosts_SelectedIndexChanged;
    }


    protected override void OnShown(EventArgs e)
    {
      _initHandlers();

      //_commands = Manager.Instance.LoadCommands();
      //_cmbCommand.Items.Clear();
      //foreach(var command in _commands)
      //{
      //  _cmbCommand.Items.Add(command.cmd);
      //}
      //_cmbCommand.SelectedIndex = 0;
      _enableDisable();

      base.OnShown(e);
    }
    private void _initHandlers()
    {
      _manager.OnStopped += _manager_OnStopped;
      _manager.OnStarted += _manager_OnStarted;
      //_manager.OnMessage += _setTraceText;
      _manager.WSServer.OnWSUserStateChanged += WSServer_OnWSUserStateChanged;
      _manager.InitHandlers();

      _cmdRun.Click += _cmdRun_Click;
      //_cmdSettings.Click += _cmdSettings_Click;
      _cmdServerLogFolder.Click += _cmdServerLogFolder_Click;
      _updateControls();

    }

    private void _cmdServerLogFolder_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
      {
        //browserDialog.RootFolder = _txtServerLogFolder.Text;
        if(browserDialog.ShowDialog(this)== DialogResult.OK)
        {
          _txtServerLogFolder.Text = browserDialog.SelectedPath;
        }

      }
    }

    private void _manager_OnStarted()
    {
      if (_cmdRun.InvokeRequired)
      {
        Manager.EventManagerStarted d = new Manager.EventManagerStarted(_manager_OnStarted);
        Invoke(d);
      }
      else
      {
        _cmdRun.Text = "STOP";
        _cmdRun.Enabled = true;
        _enableDisable();
        Cursor = Cursors.Default;
      }

    }
    void _manager_OnStopped()
    {
      if (_cmdRun.InvokeRequired)
      {
        Manager.EventManagerStarted d = new Manager.EventManagerStarted(_manager_OnStopped);
        Invoke(d);
      }
      else
      {
        _isRunning = false;
        _cmdRun.Text = "START";
        _cmdRun.Enabled = true;
        _lstUsers.Items.Clear();
        _enableDisable();
        Cursor = Cursors.Default;
      }
    }
    /*
    void _cmdSettings_Click(object sender, EventArgs e)
    {
      using (frmSettings f = new frmSettings())
      {
        f.ShowDialog(this);
      }
    }
    */
    private ListViewItem _addUserInfo(WSClientInfo info)
    {
      ListViewItem item = _lstUsers.Items.Add(info.UserName);
      item.SubItems.Add(info.WIN);
      item.SubItems.Add(info.Command.ToString());
      item.SubItems.Add(info.Pulse.ToString());
      return item;
    }
    void WSServer_OnWSUserStateChanged(WSClientInfo info)
    {
      if (_lstUsers.InvokeRequired)
      {
        EventWSUserStateChanged d = new EventWSUserStateChanged(WSServer_OnWSUserStateChanged);
        this.Invoke(d, new object[] { info });
      }
      else
      {
        for (int i = 0; i < _lstUsers.Items.Count; i++)
        {
          ListViewItem item = _lstUsers.Items[i];
          if (item.Text == info.UserName && item.SubItems[1].Text == info.WIN)
          {
            if (info.Command == WSClientStatus.closed || info.Command == WSClientStatus.unreg)
            {
              item.Remove();
            }
            else
            {
              item.SubItems[2].Text = info.Command.ToString();
              item.SubItems[3].Text = info.Pulse.ToString();
            }
            return;
          }
        }
        _addUserInfo(info);
      }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (_isRunning)
      {
        if (DialogResult.OK == MessageBox.Show("Server is running! Stop the server?", "Warning", MessageBoxButtons.OKCancel))
        {
          _managerStop();
         // e.Cancel = true;
         // return;
        }
        else
        {
          e.Cancel = true;
          return;
        }
      }
      base.OnClosing(e);
    }
    private void _managerStop()
    {
      _manager.Stop();

    }
    private void _cmdRun_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      if (_isRunning)
      {
        _managerStop();
      }
      else
      {
        _updateData();
        _isRunning = true;
        _cmdRun.Enabled = false;
        _manager.Start();
      }
    }
    /*
    delegate void SetTextCallback(string text);
    private void _setTraceText(string text)
    {
      if (_txtTrace.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(_setTraceText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        _txtTrace.Text += text + Environment.NewLine; ;
      }
    }
    */
    private void _cmdSend_Click(object sender, EventArgs e)
    {
      if (_checkMarkUsers())
      {
        //List<TAPIParameter> p = new List<TAPIParameter>();
        ////   p.Add(new TAPIPerameter() { Id = "X11", Value = "Привет мир" });
        ////   p.Add(new TAPIPerameter() { Id = "X13", Value = "Привет мир2222" });

        //for (int i = 0; i < _lstParameters.Items.Count; i++)
        //{
        //  ListViewItem item = _lstParameters.Items[i];
        //  p.Add(new TAPIParameter() { Id = item.Text, Value = item.SubItems[1].Text });
        //}
        //_manager.SetTAPIMessage(_cmbCommand.Text, p);
      }
      else
      {
        MessageBox.Show("No selected users!", "Warning");
      }
    }

    private void _lstUsers_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      ListViewItem item = _lstUsers.Items[e.Index];
      string userName =  item.SubItems[0].Text;
      string win =  item.SubItems[1].Text;
      bool can = e.NewValue == CheckState.Checked;
      _manager.WSServer.SetCanReceiveMessage(userName, win,can);
    }
    /*
    private void _cmdClear_Click(object sender, EventArgs e)
    {
      if (_txtTrace.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(_setTraceText);
        this.Invoke(d, new object[] { "" });
      }
      else
      {
        _txtTrace.Text = ""; ;
      }
    }
    */
    private bool _checkMarkUsers()
    {
      bool result = false;
      for (int i = 0; i < _lstUsers.Items.Count; i++)
      {
        ListViewItem item = _lstUsers.Items[i];
        if (item.Checked)
        {
          result = true;
          break;
        }
      }
      return result;
    }

    private void _enableDisable()
    {
      _cmdSend.Enabled = _isRunning;
      _cmdSave.Enabled = !_isRunning;
      _txtHttpPort.Enabled = !_isRunning;
      _txtFieldId.Enabled = !_isRunning;
      _txtMenuId.Enabled = !_isRunning;
      _txtServerLogFolder.Enabled = !_isRunning;
      _cmbHttpHost.Enabled = !_isRunning;
      _chkFullLogMode.Enabled = !_isRunning;
      //  _cmdCisco.Enabled = !_isRunning;
    }

    private void _cmdPrmSave_Click(object sender, EventArgs e)
    {
      if (_lstParameters.SelectedItems.Count > 0)
      {
        ListViewItem item = _lstParameters.SelectedItems[0];
        item.SubItems[1].Text = _txtPrmEdit.Text;
      }
    }

    frmLog _formLogView;
    private void _cmdLogFile_Click(object sender, EventArgs e)
    {
      if(_formLogView == null)
      {
        _formLogView = new frmLog();
      }
      if (_formLogView.Visible)
      {
        _formLogView.Hide();
      }
      else
      {
        _formLogView.RefreshLogContent();
        _formLogView.Show(this);
      }
    }
    #region Settings

    private void _cmbCiscoHosts_SelectedIndexChanged(object sender, EventArgs e)
    {
      _txtCiscoSelected.Text = _cmbCiscoHosts.SelectedItem.ToString();
    }

    private void _cmbCommand_SelectedIndexChanged(object sender, EventArgs e)
    {
      int index = _cmbCommand.SelectedIndex;
      _lstParameters.Items.Clear();
      //List<TAPIParameter> prms = _commands[index].p;
      //foreach (var p in prms)
      //{
      //  ListViewItem item = _lstParameters.Items.Add(p.Id);
      //  item.SubItems.Add(p.Value);
      //}
      _txtPrmEdit.Text = "";
    }

    private void _updateCiscoHostsCombo()
    {
      _cmbCiscoHosts.Items.Clear();

      string[] hosts = _txtCiscoHosts.Text.Split(';');
      string first = null;
      foreach (string host in hosts)
      {
        string s = host.Trim();
        if (!string.IsNullOrEmpty(s))
        {
          if(first == null)
          {
            first = s;
          }
          _cmbCiscoHosts.Items.Add(s);
        }
      }
      _cmbCiscoHosts.SelectedItem = _txtCiscoSelected.Text == "" ? first : _txtCiscoSelected.Text;
      if(_cmbCiscoHosts.SelectedItem != null)
      {
        _txtCiscoSelected.Text = _cmbCiscoHosts.SelectedItem.ToString();
      }
      else
      {
        _cmbCiscoHosts.SelectedIndex = 0;
        //_txtCiscoSelected.Text = "";
      }
    }
    private void _updateControls()
    {
      ServerSettings s = Manager.Instance.Settings;
      s.LoadConfig();

      List<string> lstIp = _getListIPAddress();

      _cmbHttpHost.DataSource = lstIp;
      _cmbHttpHost.SelectedItem = s.MarabuHost;

      _txtHttpPort.Text = s.MarabuPort;

      _txtMenuId.Text = s.MenuItemId;
      _txtFieldId.Text = s.FieldId;

      _txtCiscoHosts.Text = s.CiscoHosts;
      _txtCiscoBindPort.Text = s.CiscoNotifierPort;
      _txtCiscoPort.Text = s.CiscoPort;
      _txtServerLogFolder.Text = s.ServerLogFolder;

      _chkFullLogMode.Checked = s.FullLogMode;

      _updateCiscoHostsCombo();
    }
    private void _updateData()
    {
      ServerSettings s = Manager.Instance.Settings;
      s.MarabuHost = _cmbHttpHost.Text;
      s.MarabuPort = _txtHttpPort.Text;

      s.MenuItemId = _txtMenuId.Text;
      s.FieldId = _txtFieldId.Text;


      s.CiscoHosts = _txtCiscoHosts.Text;
      s.CiscoNotifierPort = _txtCiscoBindPort.Text;
      s.CiscoPort = _txtCiscoPort.Text;
      s.CiscoSelectedHost = _txtCiscoSelected.Text;
      s.ServerLogFolder = _txtServerLogFolder.Text;
      s.FullLogMode = _chkFullLogMode.Checked;

      _updateCiscoHostsCombo();

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
        if (!s.Contains(":"))
        {
          lst_str.Add(s);
        }
      }
      return lst_str;
    }

    private void _cmdSave_Click(object sender, EventArgs e)
    {
      _updateData();

    }
    #endregion

    private void _lstParameters_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_lstParameters.SelectedItems.Count > 0)
      {
        ListViewItem item = _lstParameters.SelectedItems[0];
        string s = item.SubItems[1].Text;
        _txtPrmEdit.Text = s.ToString();
      }
    }

    private void _cmdHttpUI_Click(object sender, EventArgs e)
    {
      Manager.Instance.launchWebUI();
    }

    private void _cmdCisco_Click(object sender, EventArgs e)
    {
      using(frmUsers f = new frmUsers())
      {
        f.ShowDialog(this);
      }
    }
  }//
}
