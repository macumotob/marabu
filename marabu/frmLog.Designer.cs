namespace marabu
{
  partial class frmLog
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
      this._txtLog = new System.Windows.Forms.TextBox();
      this._cmdRefresh = new System.Windows.Forms.Button();
      this._cdmClearLogFile = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // _txtLog
      // 
      this._txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this._txtLog.Location = new System.Drawing.Point(0, 2);
      this._txtLog.Multiline = true;
      this._txtLog.Name = "_txtLog";
      this._txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this._txtLog.Size = new System.Drawing.Size(710, 220);
      this._txtLog.TabIndex = 0;
      this._txtLog.WordWrap = false;
      // 
      // _cmdRefresh
      // 
      this._cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdRefresh.Location = new System.Drawing.Point(605, 229);
      this._cmdRefresh.Name = "_cmdRefresh";
      this._cmdRefresh.Size = new System.Drawing.Size(94, 20);
      this._cmdRefresh.TabIndex = 1;
      this._cmdRefresh.Text = "Refresh";
      this._cmdRefresh.UseVisualStyleBackColor = true;
      this._cmdRefresh.Click += new System.EventHandler(this._cmdRefresh_Click);
      // 
      // _cdmClearLogFile
      // 
      this._cdmClearLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cdmClearLogFile.Location = new System.Drawing.Point(505, 229);
      this._cdmClearLogFile.Name = "_cdmClearLogFile";
      this._cdmClearLogFile.Size = new System.Drawing.Size(94, 20);
      this._cdmClearLogFile.TabIndex = 2;
      this._cdmClearLogFile.Text = "Clear Log File";
      this._cdmClearLogFile.UseVisualStyleBackColor = true;
      this._cdmClearLogFile.Click += new System.EventHandler(this._cdmClearLogFile_Click);
      // 
      // frmLog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(711, 261);
      this.Controls.Add(this._cdmClearLogFile);
      this.Controls.Add(this._cmdRefresh);
      this.Controls.Add(this._txtLog);
      this.Name = "frmLog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "HP TAPI Log ";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox _txtLog;
    private System.Windows.Forms.Button _cmdRefresh;
    private System.Windows.Forms.Button _cdmClearLogFile;
  }
}