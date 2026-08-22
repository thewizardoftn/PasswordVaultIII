using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using PasswordVaultIII.Data;

namespace PasswordVaultIII
{
    public partial class frmNew : Form
    {
        private const string PasswordChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";

        private readonly VaultRepository _repo;

        public frmNew(VaultRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void frmNew_Load(object sender, EventArgs e)
        {
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            txtPass.Text = GenerateRandomPassword(20);
        }

        private static string GenerateRandomPassword(int length)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            var sb = new StringBuilder(length);
            foreach (byte b in bytes)
            {
                sb.Append(PasswordChars[b % PasswordChars.Length]);
            }
            return sb.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(this, "Please enter a site name.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_repo.NameUrlExists(txtName.Text, txtURL.Text))
                {
                    MessageBox.Show(this, "This entry already exists", "User Error!");
                    return;
                }

                _repo.Insert(new VaultEntry
                {
                    Name = txtName.Text,
                    Url = txtURL.Text,
                    Login = txtLogin.Text,
                    Password = txtPass.Text,
                    Notes = txtNotes.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
