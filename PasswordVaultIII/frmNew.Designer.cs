namespace PasswordVaultIII
{
    partial class frmNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNew));
            label1 = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            txtURL = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtLogin = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtPass = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            btnCreate = new System.Windows.Forms.Button();
            txtNotes = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(64, 15);
            label1.TabIndex = 0;
            label1.Text = "Site Name:";
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(90, 2);
            txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(250, 23);
            txtName.TabIndex = 1;
            // 
            // txtURL
            // 
            txtURL.Location = new System.Drawing.Point(90, 32);
            txtURL.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtURL.Name = "txtURL";
            txtURL.Size = new System.Drawing.Size(250, 23);
            txtURL.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 40);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "URL:";
            // 
            // txtLogin
            // 
            txtLogin.Location = new System.Drawing.Point(90, 62);
            txtLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new System.Drawing.Size(250, 23);
            txtLogin.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(14, 70);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(68, 15);
            label3.TabIndex = 4;
            label3.Text = "User Name:";
            // 
            // txtPass
            // 
            txtPass.Location = new System.Drawing.Point(90, 92);
            txtPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtPass.Name = "txtPass";
            txtPass.Size = new System.Drawing.Size(250, 23);
            txtPass.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(14, 100);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(60, 15);
            label4.TabIndex = 6;
            label4.Text = "Password:";
            // 
            // btnCreate
            // 
            btnCreate.Location = new System.Drawing.Point(349, 88);
            btnCreate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(88, 27);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtNotes
            // 
            txtNotes.Location = new System.Drawing.Point(18, 149);
            txtNotes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new System.Drawing.Size(322, 139);
            txtNotes.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(14, 130);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(41, 15);
            label5.TabIndex = 9;
            label5.Text = "Notes:";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(349, 233);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(88, 27);
            button1.TabIndex = 11;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(349, 262);
            btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(88, 27);
            btnClose.TabIndex = 12;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmNew
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(446, 302);
            Controls.Add(btnClose);
            Controls.Add(button1);
            Controls.Add(txtNotes);
            Controls.Add(label5);
            Controls.Add(btnCreate);
            Controls.Add(txtPass);
            Controls.Add(label4);
            Controls.Add(txtLogin);
            Controls.Add(label3);
            Controls.Add(txtURL);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "frmNew";
            Text = "Create a New Contact";
            Load += frmNew_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
    }
}