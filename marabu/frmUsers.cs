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
  public partial class frmUsers : Form
  {
    public frmUsers()
    {
      InitializeComponent();
      _cmdSave.Click += _cmdSave_Click;
      _cmdShowFolder.Click += _cmdShowFolder_Click;
      _txtPath.Text = HpsmCisco.Instance.UsersFileName;
      _cmdCancel.Click += _cmdCancel_Click;
    }

    private void _cmdSave_Click(object sender, EventArgs e)
    {
      HpsmCisco.Instance.Save(_txtUsers.Text);
      this.Close();
    }

    protected override void OnShown(EventArgs e)
    {
      _txtUsers.Text = HpsmCisco.Instance.GetFileContent();
      base.OnShown(e);
    }

    private void _cmdShowFolder_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      int pos = _txtPath.Text.LastIndexOf("\\") + 1;
      openFileDialog1.InitialDirectory = _txtPath.Text.Substring(0, pos);
      openFileDialog1.FileName = _txtPath.Text.Substring(pos);
      openFileDialog1.ShowDialog();
    }
    private void _cmdCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
