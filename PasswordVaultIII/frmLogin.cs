using System;
using System.Windows.Forms;
using PasswordVaultIII.Data;

namespace PasswordVaultIII
{
    public partial class frmLogin : Form
    {
        private enum Mode
        {
            Unlock,
            CreateVault,
        }

        private readonly VaultRepository _repo;
        private Mode _mode;

        public frmLogin(VaultRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            SetMode(_repo.HasMasterPassword() ? Mode.Unlock : Mode.CreateVault);
        }

        private void SetMode(Mode mode)
        {
            _mode = mode;
            lblMessage.Text = string.Empty;

            bool showConfirm = mode == Mode.CreateVault;

            txtPassword.Text = string.Empty;
            txtConfirm.Text = string.Empty;

            lblConfirm.Visible = showConfirm;
            txtConfirm.Visible = showConfirm;

            switch (mode)
            {
                case Mode.CreateVault:
                    lblHeading.Text = "Create a Master Password";
                    btnOk.Text = "Create Vault";
                    lblPassword.Text = "Master Password:";
                    txtPassword.Focus();
                    break;
                case Mode.Unlock:
                    lblHeading.Text = "Unlock Password Vault";
                    btnOk.Text = "Unlock";
                    lblPassword.Text = "Master Password:";
                    txtPassword.Focus();
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case Mode.CreateVault: CreateVault(); break;
                case Mode.Unlock: Unlock(); break;
            }
        }

        private void Unlock()
        {
            if (_repo.TryUnlock(txtPassword.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            lblMessage.Text = "Incorrect password.";
            txtPassword.SelectAll();
            txtPassword.Focus();
        }

        private void CreateVault()
        {
            if (txtPassword.Text.Length < 8)
            {
                lblMessage.Text = "Use at least 8 characters.";
                return;
            }
            if (txtPassword.Text != txtConfirm.Text)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            _repo.CreateMasterPassword(txtPassword.Text);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
