using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordVaultIII.Security
{
    // AES-256-GCM field encryption with a PBKDF2-derived key. Every encrypted value
    // stores its own random nonce + auth tag alongside the ciphertext so entries can't
    // be swapped or tampered with without detection.
    public static class VaultCrypto
    {
        public const int SaltSize = 16;
        public const int KeySize = 32;
        public const int NonceSize = 12;
        public const int TagSize = 16;
        public const int DefaultIterations = 600_000;

        public static byte[] NewSalt() => RandomNumberGenerator.GetBytes(SaltSize);

        public static byte[] DeriveKey(string password, byte[] salt, int iterations)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(KeySize);
        }

        public static string Encrypt(string plaintext, byte[] key)
        {
            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext ?? string.Empty);
            byte[] cipherBytes = new byte[plainBytes.Length];
            byte[] tag = new byte[TagSize];

            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Encrypt(nonce, plainBytes, cipherBytes, tag);
            }

            byte[] combined = new byte[NonceSize + TagSize + cipherBytes.Length];
            Buffer.BlockCopy(nonce, 0, combined, 0, NonceSize);
            Buffer.BlockCopy(tag, 0, combined, NonceSize, TagSize);
            Buffer.BlockCopy(cipherBytes, 0, combined, NonceSize + TagSize, cipherBytes.Length);
            return Convert.ToBase64String(combined);
        }

        public static string Decrypt(string base64, byte[] key)
        {
            if (string.IsNullOrEmpty(base64)) return string.Empty;

            byte[] combined = Convert.FromBase64String(base64);
            byte[] nonce = new byte[NonceSize];
            byte[] tag = new byte[TagSize];
            byte[] cipherBytes = new byte[combined.Length - NonceSize - TagSize];
            Buffer.BlockCopy(combined, 0, nonce, 0, NonceSize);
            Buffer.BlockCopy(combined, NonceSize, tag, 0, TagSize);
            Buffer.BlockCopy(combined, NonceSize + TagSize, cipherBytes, 0, cipherBytes.Length);

            byte[] plainBytes = new byte[cipherBytes.Length];
            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Decrypt(nonce, cipherBytes, tag, plainBytes);
            }

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
