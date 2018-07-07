namespace marabu
{
  partial class frmMain
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
            this.label10 = new System.Windows.Forms.Label();
            this._txtServerLogFolder = new System.Windows.Forms.TextBox();
            this._cmdServerLogFolder = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._cmbHttpHost = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._txtHttpPort = new System.Windows.Forms.TextBox();
            this._cmdCisco = new System.Windows.Forms.Button();
            this._cmdHttpUI = new System.Windows.Forms.Button();
            this._cmdSave = new System.Windows.Forms.Button();
            this._cmdLogFile = new System.Windows.Forms.Button();
            this._cmdRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Server Log Folder";
            // 
            // _txtServerLogFolder
            // 
            this._txtServerLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtServerLogFolder.Location = new System.Drawing.Point(118, 202);
            this._txtServerLogFolder.Name = "_txtServerLogFolder";
            this._txtServerLogFolder.Size = new System.Drawing.Size(375, 20);
            this._txtServerLogFolder.TabIndex = 31;
            // 
            // _cmdServerLogFolder
            // 
            this._cmdServerLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdServerLogFolder.Location = new System.Drawing.Point(499, 202);
            this._cmdServerLogFolder.Name = "_cmdServerLogFolder";
            this._cmdServerLogFolder.Size = new System.Drawing.Size(28, 23);
            this._cmdServerLogFolder.TabIndex = 32;
            this._cmdServerLogFolder.Text = "...";
            this._cmdServerLogFolder.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._cmdCisco);
            this.splitContainer1.Panel1.Controls.Add(this._cmdHttpUI);
            this.splitContainer1.Panel1.Controls.Add(this._cmdSave);
            this.splitContainer1.Panel1.Controls.Add(this._cmdLogFile);
            this.splitContainer1.Panel1.Controls.Add(this._cmdRun);
            this.splitContainer1.Panel1.Controls.Add(this._cmbHttpHost);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this._txtHttpPort);
            this.splitContainer1.Panel1.Controls.Add(this._cmdServerLogFolder);
            this.splitContainer1.Panel1.Controls.Add(this._txtServerLogFolder);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Size = new System.Drawing.Size(551, 361);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 6;
            // 
            // _cmbHttpHost
            // 
            this._cmbHttpHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbHttpHost.FormattingEnabled = true;
            this._cmbHttpHost.Location = new System.Drawing.Point(80, 29);
            this._cmbHttpHost.Name = "_cmbHttpHost";
            this._cmbHttpHost.Size = new System.Drawing.Size(147, 21);
            this._cmbHttpHost.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Marabu Port";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Marabu Host";
            // 
            // _txtHttpPort
            // 
            this._txtHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._txtHttpPort.Location = new System.Drawing.Point(80, 56);
            this._txtHttpPort.Name = "_txtHttpPort";
            this._txtHttpPort.Size = new System.Drawing.Size(147, 20);
            this._txtHttpPort.TabIndex = 34;
            // 
            // _cmdCisco
            // 
            this._cmdCisco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdCisco.Location = new System.Drawing.Point(200, 160);
            this._cmdCisco.Name = "_cmdCisco";
            this._cmdCisco.Size = new System.Drawing.Size(87, 21);
            this._cmdCisco.TabIndex = 41;
            this._cmdCisco.Text = "Users";
            this._cmdCisco.UseVisualStyleBackColor = true;
            // 
            // _cmdHttpUI
            // 
            this._cmdHttpUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdHttpUI.Location = new System.Drawing.Point(107, 160);
            this._cmdHttpUI.Name = "_cmdHttpUI";
            this._cmdHttpUI.Size = new System.Drawing.Size(87, 21);
            this._cmdHttpUI.TabIndex = 40;
            this._cmdHttpUI.Text = "Navigate WEB UI";
            this._cmdHttpUI.UseVisualStyleBackColor = true;
            // 
            // _cmdSave
            // 
            this._cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdSave.Location = new System.Drawing.Point(317, 72);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.Size = new System.Drawing.Size(101, 21);
            this._cmdSave.TabIndex = 39;
            this._cmdSave.Text = "Save";
            this._cmdSave.UseVisualStyleBackColor = true;
            // 
            // _cmdLogFile
            // 
            this._cmdLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdLogFile.Location = new System.Drawing.Point(14, 160);
            this._cmdLogFile.Name = "_cmdLogFile";
            this._cmdLogFile.Size = new System.Drawing.Size(87, 21);
            this._cmdLogFile.TabIndex = 38;
            this._cmdLogFile.Text = "View Log";
            this._cmdLogFile.UseVisualStyleBackColor = true;
            // 
            // _cmdRun
            // 
            this._cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdRun.Location = new System.Drawing.Point(317, 33);
            this._cmdRun.Name = "_cmdRun";
            this._cmdRun.Size = new System.Drawing.Size(98, 21);
            this._cmdRun.TabIndex = 37;
            this._cmdRun.Text = "Run Server";
            this._cmdRun.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 397);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marabu 2.0";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox _txtServerLogFolder;
        private System.Windows.Forms.Button _cmdServerLogFolder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button _cmdCisco;
        private System.Windows.Forms.Button _cmdHttpUI;
        private System.Windows.Forms.Button _cmdSave;
        private System.Windows.Forms.Button _cmdLogFile;
        private System.Windows.Forms.Button _cmdRun;
        private System.Windows.Forms.ComboBox _cmbHttpHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _txtHttpPort;
    }
}