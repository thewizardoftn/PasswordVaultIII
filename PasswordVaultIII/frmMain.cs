using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PasswordVaultIII.Data;

namespace PasswordVaultIII
{
    public partial class frmMain : Form
    {
        // Which letters land in each tab, keyed by the tab page's Name.
        private static readonly (string TabName, string Letters)[] Buckets =
        {
            ("tbABC", "ABC"), ("tbDEF", "DEF"), ("tbGHI", "GHI"), ("tbJKL", "JKL"),
            ("tbMNO", "MNO"), ("tbPQR", "PQR"), ("tbSTU", "STU"), ("tbVW", "VW"),
            ("tbXYZ", "XYZ"), ("tbNum", "1234567890"),
        };

        private readonly ContextMenuStrip _rowContextMenu = new ContextMenuStrip();
        private readonly VaultRepository _repo;

        private System.Collections.Generic.List<VaultEntry> _entries = new();
        private DataGridView _dgv;

        public DataGridView dgvSet
        {
            get { return _dgv; }
            set { _dgv = value; }
        }

        public frmMain(VaultRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _rowContextMenu.Items.Add("New", null, NewEntry);
            _rowContextMenu.Items.Add("Delete", null, Delete);
            _rowContextMenu.Items.Add("Update", null, Edit);
            _rowContextMenu.Items.Add("Open URL", null, OpenUrl);
            _rowContextMenu.Items.Add("Notes", null, ShowNotes);

            this.TopMost = true;
            tcA.SelectedIndex = 0;
            tcA.Selecting += tca_Selecting;
            dgvSet = dgvABC;
            dgvSet.MouseUp += dgvSet_MouseUp;

            LoadAllEntries();
            BindGrid(dgvABC, "tbABC");
        }

        private void LoadAllEntries()
        {
            try
            {
                _entries = _repo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Loading Vault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _entries = new System.Collections.Generic.List<VaultEntry>();
            }
        }

        private static string GetLetters(string tabName)
        {
            foreach (var bucket in Buckets)
            {
                if (bucket.TabName == tabName) return bucket.Letters;
            }
            return string.Empty;
        }

        private void BindGrid(DataGridView dgv, string tabName)
        {
            string letters = GetLetters(tabName);
            var filtered = _entries
                .Where(e => e.Name.Length > 0 && letters.IndexOf(char.ToUpperInvariant(e.Name[0])) >= 0)
                .ToList();

            dgv.AutoGenerateColumns = false;
            dgv.DataSource = filtered;
        }

        private void RefreshCurrentTab()
        {
            LoadAllEntries();
            BindGrid(dgvSet, tcA.SelectedTab.Name);
        }

        private void OpenUrl(object sender, EventArgs e)
        {
            if (dgvSet.CurrentRow?.DataBoundItem is not VaultEntry entry) return;
            if (string.IsNullOrWhiteSpace(entry.Url)) return;

            try
            {
                Process.Start(new ProcessStartInfo(entry.Url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not open URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewEntry(object sender, EventArgs e)
        {
            using var frm = new frmNew(_repo);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                RefreshCurrentTab();
            }
        }

        private void Delete(object sender, EventArgs e)
        {
            if (dgvSet.CurrentRow?.DataBoundItem is not VaultEntry entry) return;

            var confirm = MessageBox.Show(this, $"Delete '{entry.Name}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _repo.Delete(entry.Id);
                RefreshCurrentTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Edit(object sender, EventArgs e)
        {
            dgvSet.EndEdit();
            if (dgvSet.CurrentRow?.DataBoundItem is not VaultEntry entry) return;

            try
            {
                _repo.Update(entry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowNotes(object sender, EventArgs e)
        {
            if (dgvSet.CurrentRow?.DataBoundItem is not VaultEntry entry) return;

            using var frm = new frmNotes(_repo, entry);
            frm.ShowDialog(this);
        }

        private void dgvSet_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = dgvSet.HitTest(e.X, e.Y);
            if (hit.Type != DataGridViewHitTestType.Cell) return;

            dgvSet.CurrentCell = dgvSet.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
            _rowContextMenu.Show(dgvSet, new Point(e.X, e.Y));
        }

        private void tca_Selecting(object sender, TabControlCancelEventArgs e)
        {
            foreach (Control c in e.TabPage.Controls)
            {
                if (c is DataGridView dgv)
                {
                    dgvSet.MouseUp -= dgvSet_MouseUp;
                    dgvSet = dgv;
                    dgvSet.MouseUp += dgvSet_MouseUp;
                    BindGrid(dgv, e.TabPage.Name);
                    break;
                }
            }
        }
    }
}
