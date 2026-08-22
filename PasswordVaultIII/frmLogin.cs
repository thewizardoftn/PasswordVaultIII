using System;
using System.IO;
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
            RecoveryEntry,
            ResetAfterRecovery,
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

            bool showPassword = mode == Mode.CreateVault || mode == Mode.Unlock || mode == Mode.ResetAfterRecovery;
            bool showConfirm = mode == Mode.CreateVault || mode == Mode.ResetAfterRecovery;
            bool showRecovery = mode == Mode.RecoveryEntry;

            lblPassword.Visible = showPassword;
            txtPassword.Visible = showPassword;
            lblConfirm.Visible = showConfirm;
            txtConfirm.Visible = showConfirm;
            lblRecoveryKey.Visible = showRecovery;
            txtRecoveryKey.Visible = showRecovery;
            lnkForgotPassword.Visible = mode == Mode.Unlock;
            lnkBack.Visible = mode == Mode.RecoveryEntry;

            txtPassword.Text = string.Empty;
            txtConfirm.Text = string.Empty;
            txtRecoveryKey.Text = string.Empty;

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
                case Mode.RecoveryEntry:
                    lblHeading.Text = "Enter Recovery Key";
                    btnOk.Text = "Continue";
                    txtRecoveryKey.Focus();
                    break;
                case Mode.ResetAfterRecovery:
                    lblHeading.Text = "Set a New Master Password";
                    btnOk.Text = "Save New Password";
                    lblPassword.Text = "New Master Password:";
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
                case Mode.RecoveryEntry: SubmitRecoveryKey(); break;
                case Mode.ResetAfterRecovery: SubmitNewPassword(); break;
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

            string recoveryKey = _repo.CreateMasterPassword(txtPassword.Text);
            OfferLegacyImport();

            using (var frm = new frmRecoveryKey(recoveryKey))
            {
                frm.ShowDialog(this);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetMode(Mode.RecoveryEntry);
        }

        private void lnkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetMode(Mode.Unlock);
        }

        private void SubmitRecoveryKey()
        {
            if (_repo.TryUnlockWithRecoveryKey(txtRecoveryKey.Text))
            {
                SetMode(Mode.ResetAfterRecovery);
                return;
            }

            lblMessage.Text = "Invalid recovery key.";
            txtRecoveryKey.SelectAll();
            txtRecoveryKey.Focus();
        }

        private void SubmitNewPassword()
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

            _repo.ResetMasterPassword(txtPassword.Text);
            MessageBox.Show(this, "Your master password has been changed.", "Password Reset",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void OfferLegacyImport()
        {
            string legacyPath = FindLegacyDatabase();
            if (legacyPath == null) return;

            var choice = MessageBox.Show(this,
                "Found an existing Password Vault database from the previous version:\n" + legacyPath +
                "\n\nImport its entries into the new encrypted vault now?\n\n" +
                (LegacyPasswordDecryptor.TryCreate() != null
                    ? "Passwords will be recovered and imported along with everything else."
                    : "Note: the old app's password encryption relied on a component that isn't " +
                      "available here, so stored passwords cannot be recovered. Site names, URLs, " +
                      "logins and notes will be imported, but every entry will need its password re-entered."),
                "Import Legacy Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice != DialogResult.Yes) return;

            try
            {
                var result = LegacyAccessImporter.Import(legacyPath, _repo);
                string message = result.EntriesNeedingPassword.Count == 0
                    ? $"Imported {result.Imported} entries, including passwords."
                    : $"Imported {result.Imported} entries ({result.PasswordsRecovered} with their password recovered). " +
                      $"{result.EntriesNeedingPassword.Count} need their password re-entered by hand " +
                      "(open each entry and use \"Update\" after typing the real password back in).";
                MessageBox.Show(this, message, "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Could not read the legacy database: " + ex.Message +
                    "\n\nThis usually means the Microsoft Access Database Engine is not installed. " +
                    "You can skip the import and enter your entries manually.",
                    "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string FindLegacyDatabase()
        {
            foreach (string name in new[] { "TheVault.accdb", "TheVault.mdb", "thevault.accdb", "thevault.mdb" })
            {
                string candidate = Path.Combine(AppContext.BaseDirectory, name);
                if (File.Exists(candidate)) return candidate;
            }
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
