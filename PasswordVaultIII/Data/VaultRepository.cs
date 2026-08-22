using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;
using PasswordVaultIII.Security;

namespace PasswordVaultIII.Data
{
    // Every field is encrypted before it reaches SQLite, including the site name -
    // so entries are grouped into A-Z tabs in memory after decrypting the whole table,
    // rather than filtered with a SQL WHERE like the old Access-backed version did.
    //
    // The actual field-encryption key (the "vault key") is a random value that never
    // changes. It's never stored directly - instead it's wrapped (encrypted) once under
    // a key derived from the master password, and once under a key derived from a
    // recovery key, with each wrapped copy stored separately. That's what lets either
    // secret independently unlock the vault, and lets the master password be reset
    // (by rewrapping) without having to re-encrypt every stored entry.
    public sealed class VaultRepository
    {
        private readonly string _connectionString;
        private byte[] _vaultKey;

        public string DatabasePath { get; }

        public VaultRepository(string databasePath)
        {
            DatabasePath = databasePath;
            Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);
            _connectionString = new SqliteConnectionStringBuilder { DataSource = databasePath }.ToString();
            EnsureSchema();
        }

        private void EnsureSchema()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS VaultMeta (
                    Key TEXT PRIMARY KEY,
                    Value TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS VaultBase (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    VName TEXT NOT NULL,
                    Url TEXT NOT NULL,
                    Login TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    Notes TEXT NOT NULL
                );";
            cmd.ExecuteNonQuery();
        }

        public bool HasMasterPassword() => GetMeta("PasswordWrappedKey") != null;

        // Generates a new random vault key, wraps it under the password and under a
        // freshly generated recovery key, and returns the recovery key so the caller
        // can show it to the user once.
        public string CreateMasterPassword(string password, int iterations = VaultCrypto.DefaultIterations)
        {
            byte[] vaultKey = RandomNumberGenerator.GetBytes(VaultCrypto.KeySize);

            WrapVaultKey(vaultKey, password, iterations, "Password");

            string recoveryKey = RecoveryKeyFormat.Generate();
            WrapVaultKey(vaultKey, RecoveryKeyFormat.Normalize(recoveryKey), iterations, "Recovery");

            _vaultKey = vaultKey;
            return recoveryKey;
        }

        public bool TryUnlock(string password) => TryUnlockCore(password, "Password");

        public bool TryUnlockWithRecoveryKey(string recoveryKey) =>
            TryUnlockCore(RecoveryKeyFormat.Normalize(recoveryKey), "Recovery");

        // Rewraps the existing vault key under a new password. Only valid once the
        // vault has been unlocked (by either the old password or the recovery key) -
        // it never touches the recovery key's own wrapped copy.
        public void ResetMasterPassword(string newPassword, int iterations = VaultCrypto.DefaultIterations)
        {
            WrapVaultKey(Key, newPassword, iterations, "Password");
        }

        private void WrapVaultKey(byte[] vaultKey, string secret, int iterations, string prefix)
        {
            byte[] salt = VaultCrypto.NewSalt();
            byte[] kek = VaultCrypto.DeriveKey(secret, salt, iterations);
            string wrapped = VaultCrypto.Encrypt(Convert.ToBase64String(vaultKey), kek);
            SetMeta(prefix + "Salt", Convert.ToBase64String(salt));
            SetMeta(prefix + "Iterations", iterations.ToString());
            SetMeta(prefix + "WrappedKey", wrapped);
        }

        private bool TryUnlockCore(string secret, string prefix)
        {
            string saltB64 = GetMeta(prefix + "Salt");
            string iterStr = GetMeta(prefix + "Iterations");
            string wrapped = GetMeta(prefix + "WrappedKey");
            if (saltB64 == null || iterStr == null || wrapped == null) return false;

            byte[] salt = Convert.FromBase64String(saltB64);
            int iterations = int.Parse(iterStr);
            byte[] kek = VaultCrypto.DeriveKey(secret, salt, iterations);

            try
            {
                _vaultKey = Convert.FromBase64String(VaultCrypto.Decrypt(wrapped, kek));
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        private string GetMeta(string key)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Value FROM VaultMeta WHERE Key = $key";
            cmd.Parameters.AddWithValue("$key", key);
            return cmd.ExecuteScalar() as string;
        }

        private void SetMeta(string key, string value)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO VaultMeta (Key, Value) VALUES ($key, $value) " +
                               "ON CONFLICT(Key) DO UPDATE SET Value = excluded.Value";
            cmd.Parameters.AddWithValue("$key", key);
            cmd.Parameters.AddWithValue("$value", value);
            cmd.ExecuteNonQuery();
        }

        private byte[] Key => _vaultKey ?? throw new InvalidOperationException("Vault is locked.");

        public List<VaultEntry> GetAll()
        {
            var results = new List<VaultEntry>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, VName, Url, Login, Password, Notes FROM VaultBase";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new VaultEntry
                {
                    Id = reader.GetInt32(0),
                    Name = VaultCrypto.Decrypt(reader.GetString(1), Key),
                    Url = VaultCrypto.Decrypt(reader.GetString(2), Key),
                    Login = VaultCrypto.Decrypt(reader.GetString(3), Key),
                    Password = VaultCrypto.Decrypt(reader.GetString(4), Key),
                    Notes = VaultCrypto.Decrypt(reader.GetString(5), Key),
                });
            }
            results.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
            return results;
        }

        public bool NameUrlExists(string name, string url)
        {
            foreach (var e in GetAll())
            {
                if (string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(e.Url, url, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Insert(VaultEntry entry)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO VaultBase (VName, Url, Login, Password, Notes) VALUES ($n, $u, $l, $p, $notes)";
            cmd.Parameters.AddWithValue("$n", VaultCrypto.Encrypt(entry.Name, Key));
            cmd.Parameters.AddWithValue("$u", VaultCrypto.Encrypt(entry.Url, Key));
            cmd.Parameters.AddWithValue("$l", VaultCrypto.Encrypt(entry.Login, Key));
            cmd.Parameters.AddWithValue("$p", VaultCrypto.Encrypt(entry.Password, Key));
            cmd.Parameters.AddWithValue("$notes", VaultCrypto.Encrypt(entry.Notes, Key));
            cmd.ExecuteNonQuery();
        }

        public void Update(VaultEntry entry)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE VaultBase SET VName = $n, Url = $u, Login = $l, Password = $p WHERE Id = $id";
            cmd.Parameters.AddWithValue("$n", VaultCrypto.Encrypt(entry.Name, Key));
            cmd.Parameters.AddWithValue("$u", VaultCrypto.Encrypt(entry.Url, Key));
            cmd.Parameters.AddWithValue("$l", VaultCrypto.Encrypt(entry.Login, Key));
            cmd.Parameters.AddWithValue("$p", VaultCrypto.Encrypt(entry.Password, Key));
            cmd.Parameters.AddWithValue("$id", entry.Id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateNotes(int id, string notes)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE VaultBase SET Notes = $notes WHERE Id = $id";
            cmd.Parameters.AddWithValue("$notes", VaultCrypto.Encrypt(notes, Key));
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM VaultBase WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
