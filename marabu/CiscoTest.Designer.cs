namespace marabu
{
  partial class CiscoTest
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CiscoTest));
      this._txtHost = new System.Windows.Forms.TextBox();
      this._txtUser = new System.Windows.Forms.TextBox();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this._cmdSend = new System.Windows.Forms.Button();
      this._txtResult = new System.Windows.Forms.TextBox();
      this._txtRequest = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // _txtHost
      // 
      this._txtHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._txtHost.Location = new System.Drawing.Point(71, 12);
      this._txtHost.Name = "_txtHost";
      this._txtHost.Size = new System.Drawing.Size(299, 20);
      this._txtHost.TabIndex = 0;
      this._txtHost.Text = "https://sandboxapic.cisco.com/api/v0/policy";
      // 
      // _txtUser
      // 
      this._txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._txtUser.Location = new System.Drawing.Point(71, 38);
      this._txtUser.Name = "_txtUser";
      this._txtUser.Size = new System.Drawing.Size(98, 20);
      this._txtUser.TabIndex = 1;
      this._txtUser.Text = "parubok20011";
      // 
      // textBox3
      // 
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBox3.Location = new System.Drawing.Point(201, 38);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(169, 20);
      this.textBox3.TabIndex = 2;
      this.textBox3.Text = "pArovoz_ap_1976";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Host";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(22, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "User";
      // 
      // _cmdSend
      // 
      this._cmdSend.Location = new System.Drawing.Point(390, 12);
      this._cmdSend.Name = "_cmdSend";
      this._cmdSend.Size = new System.Drawing.Size(75, 23);
      this._cmdSend.TabIndex = 5;
      this._cmdSend.Text = "Send";
      this._cmdSend.UseVisualStyleBackColor = true;
      this._cmdSend.Click += new System.EventHandler(this._cmdSend_Click);
      // 
      // _txtResult
      // 
      this._txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._txtResult.Location = new System.Drawing.Point(12, 240);
      this._txtResult.Multiline = true;
      this._txtResult.Name = "_txtResult";
      this._txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this._txtResult.Size = new System.Drawing.Size(453, 160);
      this._txtResult.TabIndex = 6;
      // 
      // _txtRequest
      // 
      this._txtRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._txtRequest.Location = new System.Drawing.Point(15, 74);
      this._txtRequest.Multiline = true;
      this._txtRequest.Name = "_txtRequest";
      this._txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this._txtRequest.Size = new System.Drawing.Size(453, 160);
      this._txtRequest.TabIndex = 7;
      this._txtRequest.Text = resources.GetString("_txtRequest.Text");
      // 
      // CiscoTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(480, 412);
      this.Controls.Add(this._txtRequest);
      this.Controls.Add(this._txtResult);
      this.Controls.Add(this._cmdSend);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this._txtUser);
      this.Controls.Add(this._txtHost);
      this.Name = "CiscoTest";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "CiscoTest";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox _txtHost;
    private System.Windows.Forms.TextBox _txtUser;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button _cmdSend;
    private System.Windows.Forms.TextBox _txtResult;
    private System.Windows.Forms.TextBox _txtRequest;
  }
}