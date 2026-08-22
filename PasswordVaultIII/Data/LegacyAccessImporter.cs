using System.Collections.Generic;
using System.Data.OleDb;

namespace PasswordVaultIII.Data
{
    // One-time importer for the old Access-based vault (TheVault.accdb / .mdb).
    // Site name, URL, login and notes were always stored in plain text in the Access file
    // and are imported as-is. Passwords were encrypted with a cipher that lived in a
    // separate "clsPWV" library - if that compiled library (clsPWV.dll) is sitting next to
    // this app, LegacyPasswordDecryptor uses it to recover the real passwords too. If it's
    // not present, passwords are left blank and the entry is flagged so the user knows to
    // re-enter it - the same fallback this importer always had.
    public static class LegacyAccessImporter
    {
        public sealed class ImportResult
        {
            public int Imported { get; set; }
            public int PasswordsRecovered { get; set; }
            public List<string> EntriesNeedingPassword { get; } = new List<string>();
        }

        public static ImportResult Import(string accessFilePath, VaultRepository repo)
        {
            var result = new ImportResult();
            var decryptor = LegacyPasswordDecryptor.TryCreate();

            string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={accessFilePath};";

            using var conn = new OleDbConnection(connStr);
            conn.Open();
            using var cmd = new OleDbCommand("SELECT vName, URL, Login, Password, Notes FROM VaultBase", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader["vName"]?.ToString() ?? string.Empty;
                string url = reader["URL"]?.ToString() ?? string.Empty;
                string login = reader["Login"]?.ToString() ?? string.Empty;
                string encryptedPassword = reader["Password"]?.ToString() ?? string.Empty;
                string notes = reader["Notes"]?.ToString() ?? string.Empty;

                string password = string.Empty;
                if (decryptor != null && encryptedPassword.Length > 0)
                {
                    try
                    {
                        password = decryptor.Decrypt(encryptedPassword);
                        result.PasswordsRecovered++;
                    }
                    catch
                    {
                        password = string.Empty;
                    }
                }

                if (password.Length == 0)
                {
                    result.EntriesNeedingPassword.Add(name);
                }

                repo.Insert(new VaultEntry
                {
                    Name = name,
                    Url = url,
                    Login = login,
                    Password = password,
                    Notes = notes
                });

                result.Imported++;
            }

            return result;
        }
    }
}
