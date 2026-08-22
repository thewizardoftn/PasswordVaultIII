namespace PasswordVaultIII
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRecoveryKey = new System.Windows.Forms.Label();
            this.txtRecoveryKey = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.lnkBack = new System.Windows.Forms.LinkLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblHeading
            //
            this.lblHeading.AutoSize = true;
            this.lblHeading.Location = new System.Drawing.Point(12, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(100, 13);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Unlock Password Vault";
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 40);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(90, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Master Password:";
            //
            // txtPassword
            //
            this.txtPassword.Location = new System.Drawing.Point(120, 37);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 2;
            //
            // lblRecoveryKey
            //
            this.lblRecoveryKey.AutoSize = true;
            this.lblRecoveryKey.Location = new System.Drawing.Point(12, 40);
            this.lblRecoveryKey.Name = "lblRecoveryKey";
            this.lblRecoveryKey.Size = new System.Drawing.Size(90, 13);
            this.lblRecoveryKey.TabIndex = 8;
            this.lblRecoveryKey.Text = "Recovery Key:";
            this.lblRecoveryKey.Visible = false;
            //
            // txtRecoveryKey
            //
            this.txtRecoveryKey.Location = new System.Drawing.Point(120, 37);
            this.txtRecoveryKey.Name = "txtRecoveryKey";
            this.txtRecoveryKey.Size = new System.Drawing.Size(200, 20);
            this.txtRecoveryKey.TabIndex = 9;
            this.txtRecoveryKey.Visible = false;
            //
            // lblConfirm
            //
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Location = new System.Drawing.Point(12, 68);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(90, 13);
            this.lblConfirm.TabIndex = 3;
            this.lblConfirm.Text = "Confirm Password:";
            //
            // txtConfirm
            //
            this.txtConfirm.Location = new System.Drawing.Point(120, 65);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.Size = new System.Drawing.Size(200, 20);
            this.txtConfirm.TabIndex = 4;
            //
            // lblMessage
            //
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMessage.Location = new System.Drawing.Point(12, 96);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 5;
            //
            // lnkForgotPassword
            //
            this.lnkForgotPassword.AutoSize = true;
            this.lnkForgotPassword.Location = new System.Drawing.Point(12, 122);
            this.lnkForgotPassword.Name = "lnkForgotPassword";
            this.lnkForgotPassword.Size = new System.Drawing.Size(130, 13);
            this.lnkForgotPassword.TabIndex = 10;
            this.lnkForgotPassword.TabStop = true;
            this.lnkForgotPassword.Text = "Forgot your master password?";
            this.lnkForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForgotPassword_LinkClicked);
            //
            // lnkBack
            //
            this.lnkBack.AutoSize = true;
            this.lnkBack.Location = new System.Drawing.Point(12, 122);
            this.lnkBack.Name = "lnkBack";
            this.lnkBack.Size = new System.Drawing.Size(30, 13);
            this.lnkBack.TabIndex = 11;
            this.lnkBack.TabStop = true;
            this.lnkBack.Text = "Back";
            this.lnkBack.Visible = false;
            this.lnkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBack_LinkClicked);
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(164, 149);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Unlock";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(245, 149);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // frmLogin
            //
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 184);
            this.Controls.Add(this.lnkBack);
            this.Controls.Add(this.lnkForgotPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtRecoveryKey);
            this.Controls.Add(this.lblRecoveryKey);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Vault III";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRecoveryKey;
        private System.Windows.Forms.TextBox txtRecoveryKey;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkForgotPassword;
        private System.Windows.Forms.LinkLabel lnkBack;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
