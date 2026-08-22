using System;
using System.Windows.Forms;

namespace PasswordVaultIII
{
    public partial class frmRecoveryKey : Form
    {
        public frmRecoveryKey(string recoveryKey)
        {
            InitializeComponent();
            txtRecoveryKey.Text = recoveryKey;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtRecoveryKey.Text);
        }

        private void chkSaved_CheckedChanged(object sender, EventArgs e)
        {
            btnContinue.Enabled = chkSaved.Checked;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
