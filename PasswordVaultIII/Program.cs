using System;
using System.IO;
using System.Windows.Forms;
using PasswordVaultIII.Data;

namespace PasswordVaultIII
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PasswordVaultIII");
            string dbPath = Path.Combine(appDataDir, "vault.db");

            var repo = new VaultRepository(dbPath);

            using (var login = new frmLogin(repo))
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new frmMain(repo));
        }
    }
}
