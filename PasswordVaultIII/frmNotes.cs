using System;
using System.Windows.Forms;
using PasswordVaultIII.Data;

namespace PasswordVaultIII
{
    public partial class frmNotes : Form
    {
        private readonly VaultRepository _repo;
        private readonly VaultEntry _entry;

        public frmNotes(VaultRepository repo, VaultEntry entry)
        {
            InitializeComponent();
            _repo = repo;
            _entry = entry;
            txtNotes.Text = entry.Notes;
            lblID.Text = entry.Id.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _repo.UpdateNotes(_entry.Id, txtNotes.Text);
                _entry.Notes = txtNotes.Text;
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
