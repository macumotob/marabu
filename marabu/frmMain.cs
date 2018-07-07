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
            //_manager.WSServer.OnWSUserStateChanged += WSServer_OnWSUserStateChanged;
            _manager.InitHandlers();

            _cmdRun.Click += _cmdRun_Click;
            _cmdHttpUI.Click += _cmdHttpUI_Click;

            //_cmdSettings.Click += _cmdSettings_Click;
            _cmdServerLogFolder.Click += _cmdServerLogFolder_Click;
            _updateControls();

        }

        private void _cmdServerLogFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                //browserDialog.RootFolder = _txtServerLogFolder.Text;
                if (browserDialog.ShowDialog(this) == DialogResult.OK)
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


        private void _enableDisable()
        {
            _cmdSave.Enabled = !_isRunning;
            _txtHttpPort.Enabled = !_isRunning;
            _txtServerLogFolder.Enabled = !_isRunning;
            _cmbHttpHost.Enabled = !_isRunning;
        }


        frmLog _formLogView;
        private void _cmdLogFile_Click(object sender, EventArgs e)
        {
            if (_formLogView == null)
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


        private void _updateControls()
        {
            ServerSettings s = Manager.Instance.Settings;
            s.LoadConfig();

            List<string> lstIp = _getListIPAddress();

            _cmbHttpHost.DataSource = lstIp;
            _cmbHttpHost.SelectedItem = s.MarabuHost;

            _txtHttpPort.Text = s.MarabuPort;

            _txtServerLogFolder.Text = s.ServerLogFolder;
        }
        private void _updateData()
        {
            ServerSettings s = Manager.Instance.Settings;
            s.MarabuHost = _cmbHttpHost.Text;
            s.MarabuPort = _txtHttpPort.Text;
            s.ServerLogFolder = _txtServerLogFolder.Text;
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


        private void _cmdHttpUI_Click(object sender, EventArgs e)
        {
            Manager.Instance.launchWebUI();
        }

        private void _cmdCisco_Click(object sender, EventArgs e)
        {
            using (frmUsers f = new frmUsers())
            {
                f.ShowDialog(this);
            }
        }

        private void _cmbCommand_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }//
}
