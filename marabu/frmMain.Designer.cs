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
            this._cmdRun = new System.Windows.Forms.Button();
            this._cmdLogFile = new System.Windows.Forms.Button();
            this._txtHttpPort = new System.Windows.Forms.TextBox();
            this._cmdSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._cmbHttpHost = new System.Windows.Forms.ComboBox();
            this._cmdHttpUI = new System.Windows.Forms.Button();
            this._txtFieldId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._txtMenuId = new System.Windows.Forms.TextBox();
            this._cmdCisco = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this._txtServerLogFolder = new System.Windows.Forms.TextBox();
            this._cmdServerLogFolder = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cmdRun
            // 
            this._cmdRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdRun.Location = new System.Drawing.Point(523, 100);
            this._cmdRun.Name = "_cmdRun";
            this._cmdRun.Size = new System.Drawing.Size(98, 21);
            this._cmdRun.TabIndex = 9;
            this._cmdRun.Text = "Run Server";
            this._cmdRun.UseVisualStyleBackColor = true;
            // 
            // _cmdLogFile
            // 
            this._cmdLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdLogFile.Location = new System.Drawing.Point(25, 100);
            this._cmdLogFile.Name = "_cmdLogFile";
            this._cmdLogFile.Size = new System.Drawing.Size(87, 21);
            this._cmdLogFile.TabIndex = 10;
            this._cmdLogFile.Text = "View Log";
            this._cmdLogFile.UseVisualStyleBackColor = true;
            this._cmdLogFile.Click += new System.EventHandler(this._cmdLogFile_Click);
            // 
            // _txtHttpPort
            // 
            this._txtHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._txtHttpPort.Location = new System.Drawing.Point(474, 65);
            this._txtHttpPort.Name = "_txtHttpPort";
            this._txtHttpPort.Size = new System.Drawing.Size(147, 20);
            this._txtHttpPort.TabIndex = 12;
            // 
            // _cmdSave
            // 
            this._cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdSave.Location = new System.Drawing.Point(415, 100);
            this._cmdSave.Name = "_cmdSave";
            this._cmdSave.Size = new System.Drawing.Size(101, 21);
            this._cmdSave.TabIndex = 13;
            this._cmdSave.Text = "Save";
            this._cmdSave.UseVisualStyleBackColor = true;
            this._cmdSave.Click += new System.EventHandler(this._cmdSave_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(400, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Marabu Host";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Marabu Port";
            // 
            // _cmbHttpHost
            // 
            this._cmbHttpHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbHttpHost.FormattingEnabled = true;
            this._cmbHttpHost.Location = new System.Drawing.Point(474, 38);
            this._cmbHttpHost.Name = "_cmbHttpHost";
            this._cmbHttpHost.Size = new System.Drawing.Size(147, 21);
            this._cmbHttpHost.TabIndex = 11;
            // 
            // _cmdHttpUI
            // 
            this._cmdHttpUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdHttpUI.Location = new System.Drawing.Point(118, 100);
            this._cmdHttpUI.Name = "_cmdHttpUI";
            this._cmdHttpUI.Size = new System.Drawing.Size(87, 21);
            this._cmdHttpUI.TabIndex = 16;
            this._cmdHttpUI.Text = "Navigate WEB UI";
            this._cmdHttpUI.UseVisualStyleBackColor = true;
            this._cmdHttpUI.Click += new System.EventHandler(this._cmdHttpUI_Click);
            // 
            // _txtFieldId
            // 
            this._txtFieldId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtFieldId.Location = new System.Drawing.Point(118, 66);
            this._txtFieldId.Name = "_txtFieldId";
            this._txtFieldId.Size = new System.Drawing.Size(276, 20);
            this._txtFieldId.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "SM Menu Item";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "SM Field Id";
            // 
            // _txtMenuId
            // 
            this._txtMenuId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtMenuId.Location = new System.Drawing.Point(118, 39);
            this._txtMenuId.Name = "_txtMenuId";
            this._txtMenuId.Size = new System.Drawing.Size(277, 20);
            this._txtMenuId.TabIndex = 20;
            // 
            // _cmdCisco
            // 
            this._cmdCisco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._cmdCisco.Location = new System.Drawing.Point(211, 100);
            this._cmdCisco.Name = "_cmdCisco";
            this._cmdCisco.Size = new System.Drawing.Size(87, 21);
            this._cmdCisco.TabIndex = 21;
            this._cmdCisco.Text = "Users";
            this._cmdCisco.UseVisualStyleBackColor = true;
            this._cmdCisco.Click += new System.EventHandler(this._cmdCisco_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Server Log Folder";
            // 
            // _txtServerLogFolder
            // 
            this._txtServerLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtServerLogFolder.Location = new System.Drawing.Point(118, 146);
            this._txtServerLogFolder.Name = "_txtServerLogFolder";
            this._txtServerLogFolder.Size = new System.Drawing.Size(448, 20);
            this._txtServerLogFolder.TabIndex = 31;
            // 
            // _cmdServerLogFolder
            // 
            this._cmdServerLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdServerLogFolder.Location = new System.Drawing.Point(572, 146);
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
            this.splitContainer1.Location = new System.Drawing.Point(12, 174);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._cmdServerLogFolder);
            this.splitContainer1.Panel1.Controls.Add(this._txtServerLogFolder);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._cmdCisco);
            this.splitContainer1.Panel2.Controls.Add(this._txtMenuId);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this._txtFieldId);
            this.splitContainer1.Panel2.Controls.Add(this._cmdHttpUI);
            this.splitContainer1.Panel2.Controls.Add(this._cmbHttpHost);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this._cmdSave);
            this.splitContainer1.Panel2.Controls.Add(this._txtHttpPort);
            this.splitContainer1.Panel2.Controls.Add(this._cmdLogFile);
            this.splitContainer1.Panel2.Controls.Add(this._cmdRun);
            this.splitContainer1.Size = new System.Drawing.Size(624, 380);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 566);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marabu 2.0";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.Button _cmdRun;
        private System.Windows.Forms.Button _cmdLogFile;
        private System.Windows.Forms.TextBox _txtHttpPort;
        private System.Windows.Forms.Button _cmdSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cmbHttpHost;
        private System.Windows.Forms.Button _cmdHttpUI;
        private System.Windows.Forms.TextBox _txtFieldId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _txtMenuId;
        private System.Windows.Forms.Button _cmdCisco;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox _txtServerLogFolder;
        private System.Windows.Forms.Button _cmdServerLogFolder;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}