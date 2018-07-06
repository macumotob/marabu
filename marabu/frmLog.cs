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
  public partial class frmLog : Form
  {
    public frmLog()
    {
      InitializeComponent();
    }
    public bool IsAppRunning = true;
    public void RefreshLogContent()
    {
      _txtLog.Text = "TODO INCTEMENTAL LOG";//Manager.Instance.GetLogFileContent();
      _txtLog.Select(0, 0);
    }
    protected override void OnShown(EventArgs e)
    {
      //this.Text = Manager.Instance.LogFileName;
      this.Text = "TODO INCTEMENTAL LOG";//Manager.Instance.GetLogFileContent();
      base.OnShown(e);
    }
    protected override void OnClosing(CancelEventArgs e)
    {
      e.Cancel = IsAppRunning;
      if(e.Cancel)
      {
        Hide();
      }
      base.OnClosing(e);
    }

    private void _cmdRefresh_Click(object sender, EventArgs e)
    {
      RefreshLogContent();
    }

    private void _cdmClearLogFile_Click(object sender, EventArgs e)
    {
      //Manager.Instance.ClearLogFile();
      RefreshLogContent();
    }
  }
}
