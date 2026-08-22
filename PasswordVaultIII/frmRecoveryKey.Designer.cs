namespace PasswordVaultIII
{
    partial class frmRecoveryKey
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
            this.lblBody = new System.Windows.Forms.Label();
            this.txtRecoveryKey = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.chkSaved = new System.Windows.Forms.CheckBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblHeading
            //
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeading.Location = new System.Drawing.Point(12, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(160, 17);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Save Your Recovery Key";
            //
            // lblBody
            //
            this.lblBody.Location = new System.Drawing.Point(12, 34);
            this.lblBody.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(400, 60);
            this.lblBody.TabIndex = 1;
            this.lblBody.Text = "If you forget your master password, this recovery key is the only way to get ba" +
    "ck into your vault. Save it somewhere safe - a password manager, a printed cop" +
    "y, etc. It will not be shown again.";
            //
            // txtRecoveryKey
            //
            this.txtRecoveryKey.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtRecoveryKey.Location = new System.Drawing.Point(12, 100);
            this.txtRecoveryKey.Name = "txtRecoveryKey";
            this.txtRecoveryKey.ReadOnly = true;
            this.txtRecoveryKey.Size = new System.Drawing.Size(320, 29);
            this.txtRecoveryKey.TabIndex = 2;
            this.txtRecoveryKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // btnCopy
            //
            this.btnCopy.Location = new System.Drawing.Point(338, 100);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 29);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            //
            // chkSaved
            //
            this.chkSaved.AutoSize = true;
            this.chkSaved.Location = new System.Drawing.Point(12, 144);
            this.chkSaved.Name = "chkSaved";
            this.chkSaved.Size = new System.Drawing.Size(160, 17);
            this.chkSaved.TabIndex = 4;
            this.chkSaved.Text = "I have saved this recovery key";
            this.chkSaved.UseVisualStyleBackColor = true;
            this.chkSaved.CheckedChanged += new System.EventHandler(this.chkSaved_CheckedChanged);
            //
            // btnContinue
            //
            this.btnContinue.Enabled = false;
            this.btnContinue.Location = new System.Drawing.Point(338, 168);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 5;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            //
            // frmRecoveryKey
            //
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 203);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.chkSaved);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtRecoveryKey);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecoveryKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recovery Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.TextBox txtRecoveryKey;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox chkSaved;
        private System.Windows.Forms.Button btnContinue;
    }
}
