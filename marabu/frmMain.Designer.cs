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
      this._cmdSend = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this._cmdServerLogFolder = new System.Windows.Forms.Button();
      this._txtServerLogFolder = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this._txtCiscoSelected = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this._txtCiscoBindPort = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this._txtCiscoPort = new System.Windows.Forms.TextBox();
      this._cmbCiscoHosts = new System.Windows.Forms.ComboBox();
      this._txtCiscoHosts = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this._lstUsers = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._cmdCisco = new System.Windows.Forms.Button();
      this._txtMenuId = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this._txtFieldId = new System.Windows.Forms.TextBox();
      this._cmdHttpUI = new System.Windows.Forms.Button();
      this._cmbHttpHost = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this._cmdSave = new System.Windows.Forms.Button();
      this._txtHttpPort = new System.Windows.Forms.TextBox();
      this._cmdLogFile = new System.Windows.Forms.Button();
      this._cmdRun = new System.Windows.Forms.Button();
      this._cmbCommand = new System.Windows.Forms.ComboBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this._txtPrmEdit = new System.Windows.Forms.TextBox();
      this._cmdPrmSave = new System.Windows.Forms.Button();
      this._lstParameters = new System.Windows.Forms.ListView();
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._chkFullLogMode = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _cmdSend
      // 
      this._cmdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdSend.Location = new System.Drawing.Point(558, 10);
      this._cmdSend.Name = "_cmdSend";
      this._cmdSend.Size = new System.Drawing.Size(75, 21);
      this._cmdSend.TabIndex = 1;
      this._cmdSend.Text = "Send";
      this._cmdSend.UseVisualStyleBackColor = true;
      this._cmdSend.Click += new System.EventHandler(this._cmdSend_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Command";
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
      this.splitContainer1.Panel1.Controls.Add(this._chkFullLogMode);
      this.splitContainer1.Panel1.Controls.Add(this._cmdServerLogFolder);
      this.splitContainer1.Panel1.Controls.Add(this._txtServerLogFolder);
      this.splitContainer1.Panel1.Controls.Add(this.label10);
      this.splitContainer1.Panel1.Controls.Add(this._txtCiscoSelected);
      this.splitContainer1.Panel1.Controls.Add(this.label9);
      this.splitContainer1.Panel1.Controls.Add(this.label8);
      this.splitContainer1.Panel1.Controls.Add(this._txtCiscoBindPort);
      this.splitContainer1.Panel1.Controls.Add(this.label7);
      this.splitContainer1.Panel1.Controls.Add(this._txtCiscoPort);
      this.splitContainer1.Panel1.Controls.Add(this._cmbCiscoHosts);
      this.splitContainer1.Panel1.Controls.Add(this._txtCiscoHosts);
      this.splitContainer1.Panel1.Controls.Add(this.label6);
      this.splitContainer1.Panel1.Controls.Add(this._lstUsers);
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
      // _txtServerLogFolder
      // 
      this._txtServerLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtServerLogFolder.Location = new System.Drawing.Point(118, 146);
      this._txtServerLogFolder.Name = "_txtServerLogFolder";
      this._txtServerLogFolder.Size = new System.Drawing.Size(448, 20);
      this._txtServerLogFolder.TabIndex = 31;
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
      // _txtCiscoSelected
      // 
      this._txtCiscoSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._txtCiscoSelected.Location = new System.Drawing.Point(343, 196);
      this._txtCiscoSelected.Name = "_txtCiscoSelected";
      this._txtCiscoSelected.Size = new System.Drawing.Size(223, 20);
      this._txtCiscoSelected.TabIndex = 29;
      // 
      // label9
      // 
      this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(12, 200);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(97, 13);
      this.label9.TabIndex = 28;
      this.label9.Text = "Select CISCO Host";
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(204, 227);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(85, 13);
      this.label8.TabIndex = 27;
      this.label8.Text = "CISCO Bind Port";
      // 
      // _txtCiscoBindPort
      // 
      this._txtCiscoBindPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this._txtCiscoBindPort.Location = new System.Drawing.Point(298, 223);
      this._txtCiscoBindPort.Name = "_txtCiscoBindPort";
      this._txtCiscoBindPort.Size = new System.Drawing.Size(80, 20);
      this._txtCiscoBindPort.TabIndex = 26;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 227);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(61, 13);
      this.label7.TabIndex = 25;
      this.label7.Text = "CISCO Port";
      // 
      // _txtCiscoPort
      // 
      this._txtCiscoPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this._txtCiscoPort.Location = new System.Drawing.Point(118, 224);
      this._txtCiscoPort.Name = "_txtCiscoPort";
      this._txtCiscoPort.Size = new System.Drawing.Size(69, 20);
      this._txtCiscoPort.TabIndex = 24;
      // 
      // _cmbCiscoHosts
      // 
      this._cmbCiscoHosts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._cmbCiscoHosts.FormattingEnabled = true;
      this._cmbCiscoHosts.Location = new System.Drawing.Point(118, 197);
      this._cmbCiscoHosts.Name = "_cmbCiscoHosts";
      this._cmbCiscoHosts.Size = new System.Drawing.Size(219, 21);
      this._cmbCiscoHosts.TabIndex = 23;
      // 
      // _txtCiscoHosts
      // 
      this._txtCiscoHosts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtCiscoHosts.Location = new System.Drawing.Point(118, 172);
      this._txtCiscoHosts.Name = "_txtCiscoHosts";
      this._txtCiscoHosts.Size = new System.Drawing.Size(448, 20);
      this._txtCiscoHosts.TabIndex = 22;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 175);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(69, 13);
      this.label6.TabIndex = 21;
      this.label6.Text = "CISCO Hosts";
      // 
      // _lstUsers
      // 
      this._lstUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._lstUsers.CheckBoxes = true;
      this._lstUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this._lstUsers.FullRowSelect = true;
      this._lstUsers.GridLines = true;
      this._lstUsers.Location = new System.Drawing.Point(3, 3);
      this._lstUsers.MultiSelect = false;
      this._lstUsers.Name = "_lstUsers";
      this._lstUsers.Size = new System.Drawing.Size(621, 137);
      this._lstUsers.TabIndex = 5;
      this._lstUsers.UseCompatibleStateImageBehavior = false;
      this._lstUsers.View = System.Windows.Forms.View.Details;
      this._lstUsers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._lstUsers_ItemCheck);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "User";
      this.columnHeader1.Width = 160;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "WIN";
      this.columnHeader2.Width = 160;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "CMD";
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Pulse";
      this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
      // _txtMenuId
      // 
      this._txtMenuId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtMenuId.Location = new System.Drawing.Point(118, 39);
      this._txtMenuId.Name = "_txtMenuId";
      this._txtMenuId.Size = new System.Drawing.Size(277, 20);
      this._txtMenuId.TabIndex = 20;
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
      // _txtFieldId
      // 
      this._txtFieldId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtFieldId.Location = new System.Drawing.Point(118, 66);
      this._txtFieldId.Name = "_txtFieldId";
      this._txtFieldId.Size = new System.Drawing.Size(276, 20);
      this._txtFieldId.TabIndex = 17;
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
      // _cmbHttpHost
      // 
      this._cmbHttpHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cmbHttpHost.FormattingEnabled = true;
      this._cmbHttpHost.Location = new System.Drawing.Point(474, 38);
      this._cmbHttpHost.Name = "_cmbHttpHost";
      this._cmbHttpHost.Size = new System.Drawing.Size(147, 21);
      this._cmbHttpHost.TabIndex = 11;
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
      // _txtHttpPort
      // 
      this._txtHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._txtHttpPort.Location = new System.Drawing.Point(474, 65);
      this._txtHttpPort.Name = "_txtHttpPort";
      this._txtHttpPort.Size = new System.Drawing.Size(147, 20);
      this._txtHttpPort.TabIndex = 12;
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
      // _cmbCommand
      // 
      this._cmbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._cmbCommand.FormattingEnabled = true;
      this._cmbCommand.Items.AddRange(new object[] {
            "ROOT/test link",
            "ROOT/Service Catalog/Tailoring/Add Field Mapping"});
      this._cmbCommand.Location = new System.Drawing.Point(69, 10);
      this._cmbCommand.Name = "_cmbCommand";
      this._cmbCommand.Size = new System.Drawing.Size(484, 21);
      this._cmbCommand.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this._txtPrmEdit);
      this.panel1.Controls.Add(this._cmdPrmSave);
      this.panel1.Controls.Add(this._lstParameters);
      this.panel1.Location = new System.Drawing.Point(12, 37);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(624, 131);
      this.panel1.TabIndex = 10;
      // 
      // _txtPrmEdit
      // 
      this._txtPrmEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtPrmEdit.Location = new System.Drawing.Point(0, 108);
      this._txtPrmEdit.Name = "_txtPrmEdit";
      this._txtPrmEdit.Size = new System.Drawing.Size(541, 20);
      this._txtPrmEdit.TabIndex = 5;
      // 
      // _cmdPrmSave
      // 
      this._cmdPrmSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this._cmdPrmSave.Location = new System.Drawing.Point(547, 108);
      this._cmdPrmSave.Name = "_cmdPrmSave";
      this._cmdPrmSave.Size = new System.Drawing.Size(75, 21);
      this._cmdPrmSave.TabIndex = 4;
      this._cmdPrmSave.Text = "Save Value";
      this._cmdPrmSave.UseVisualStyleBackColor = true;
      this._cmdPrmSave.Click += new System.EventHandler(this._cmdPrmSave_Click);
      // 
      // _lstParameters
      // 
      this._lstParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._lstParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._lstParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
      this._lstParameters.FullRowSelect = true;
      this._lstParameters.GridLines = true;
      this._lstParameters.Location = new System.Drawing.Point(0, 0);
      this._lstParameters.Name = "_lstParameters";
      this._lstParameters.Size = new System.Drawing.Size(621, 99);
      this._lstParameters.TabIndex = 2;
      this._lstParameters.UseCompatibleStateImageBehavior = false;
      this._lstParameters.View = System.Windows.Forms.View.Details;
      this._lstParameters.SelectedIndexChanged += new System.EventHandler(this._lstParameters_SelectedIndexChanged);
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Name";
      this.columnHeader5.Width = 176;
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "Value";
      this.columnHeader6.Width = 246;
      // 
      // _chkFullLogMode
      // 
      this._chkFullLogMode.AutoSize = true;
      this._chkFullLogMode.Location = new System.Drawing.Point(406, 222);
      this._chkFullLogMode.Name = "_chkFullLogMode";
      this._chkFullLogMode.Size = new System.Drawing.Size(93, 17);
      this._chkFullLogMode.TabIndex = 33;
      this._chkFullLogMode.Text = "Full Log Mode";
      this._chkFullLogMode.UseVisualStyleBackColor = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(648, 566);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this._cmbCommand);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this._cmdSend);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Marabu 2.0";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _cmdSend;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button _cmdRun;
    private System.Windows.Forms.ListView _lstUsers;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ComboBox _cmbCommand;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button _cmdPrmSave;
    private System.Windows.Forms.ListView _lstParameters;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.TextBox _txtPrmEdit;
    private System.Windows.Forms.Button _cmdLogFile;
    private System.Windows.Forms.ComboBox _cmbHttpHost;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button _cmdSave;
    private System.Windows.Forms.TextBox _txtHttpPort;
    private System.Windows.Forms.Button _cmdHttpUI;
    private System.Windows.Forms.TextBox _txtMenuId;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox _txtFieldId;
    private System.Windows.Forms.Button _cmdCisco;
    private System.Windows.Forms.TextBox _txtCiscoHosts;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox _txtCiscoBindPort;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox _txtCiscoPort;
    private System.Windows.Forms.ComboBox _cmbCiscoHosts;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox _txtCiscoSelected;
    private System.Windows.Forms.TextBox _txtServerLogFolder;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button _cmdServerLogFolder;
    private System.Windows.Forms.CheckBox _chkFullLogMode;
  }
}