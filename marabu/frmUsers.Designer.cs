namespace marabu
{
  partial class frmUsers
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this._txtUsers = new System.Windows.Forms.TextBox();
      this._cmdSave = new System.Windows.Forms.Button();
      this._txtPath = new System.Windows.Forms.TextBox();
      this._cmdShowFolder = new System.Windows.Forms.Button();
      this._cmdCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // _txtUsers
      // 
      this._txtUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtUsers.Location = new System.Drawing.Point(12, 36);
      this._txtUsers.Multiline = true;
      this._txtUsers.Name = "_txtUsers";
      this._txtUsers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this._txtUsers.Size = new System.Drawing.Size(587, 383);
      this._txtUsers.TabIndex = 0;
      this._txtUsers.Text = "HPSM Cisco Users";
      // 
      // _cmdSave
      // 
      this._cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdSave.Location = new System.Drawing.Point(351, 429);
      this._cmdSave.Name = "_cmdSave";
      this._cmdSave.Size = new System.Drawing.Size(128, 24);
      this._cmdSave.TabIndex = 2;
      this._cmdSave.Text = "Save";
      this._cmdSave.UseVisualStyleBackColor = true;
      // 
      // _txtPath
      // 
      this._txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtPath.Location = new System.Drawing.Point(12, 10);
      this._txtPath.Name = "_txtPath";
      this._txtPath.Size = new System.Drawing.Size(586, 20);
      this._txtPath.TabIndex = 3;
      // 
      // _cmdShowFolder
      // 
      this._cmdShowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdShowFolder.Location = new System.Drawing.Point(217, 429);
      this._cmdShowFolder.Name = "_cmdShowFolder";
      this._cmdShowFolder.Size = new System.Drawing.Size(128, 24);
      this._cmdShowFolder.TabIndex = 1;
      this._cmdShowFolder.Text = "Show in folder";
      this._cmdShowFolder.UseVisualStyleBackColor = true;
      // 
      // _cmdCancel
      // 
      this._cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdCancel.Location = new System.Drawing.Point(485, 429);
      this._cmdCancel.Name = "_cmdCancel";
      this._cmdCancel.Size = new System.Drawing.Size(113, 23);
      this._cmdCancel.TabIndex = 4;
      this._cmdCancel.Text = "Cancel";
      this._cmdCancel.UseVisualStyleBackColor = true;
      // 
      // frmUsers
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(611, 464);
      this.Controls.Add(this._cmdCancel);
      this.Controls.Add(this._cmdShowFolder);
      this.Controls.Add(this._txtPath);
      this.Controls.Add(this._cmdSave);
      this.Controls.Add(this._txtUsers);
      this.Name = "frmUsers";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "HPSM Cisco Users";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox _txtUsers;
    private System.Windows.Forms.Button _cmdSave;
    private System.Windows.Forms.TextBox _txtPath;
    private System.Windows.Forms.Button _cmdShowFolder;
    private System.Windows.Forms.Button _cmdCancel;
  }
}