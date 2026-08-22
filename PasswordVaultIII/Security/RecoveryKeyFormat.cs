using System.Security.Cryptography;
using System.Text;

namespace PasswordVaultIII.Security
{
    // Human-friendly recovery codes: random bytes encoded with a base32 alphabet that
    // avoids visually similar characters (no I, L, O, U), grouped in blocks of 4 for
    // easy transcription, e.g. "XXXX-XXXX-XXXX-XXXX-XXXX-XXXX-XXXX-XXXX".
    public static class RecoveryKeyFormat
    {
        private const string Alphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
        private const int KeyBytes = 20;

        public static string Generate()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(KeyBytes);
            return Group(Encode(bytes));
        }

        // Strips dashes/whitespace and uppercases, so a key can be derived consistently
        // regardless of how the user typed it back in.
        public static string Normalize(string input)
        {
            var sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsWhiteSpace(c) || c == '-') continue;
                sb.Append(char.ToUpperInvariant(c));
            }
            return sb.ToString();
        }

        private static string Encode(byte[] data)
        {
            var sb = new StringBuilder();
            int buffer = 0, bitsLeft = 0;
            foreach (byte b in data)
            {
                buffer = (buffer << 8) | b;
                bitsLeft += 8;
                while (bitsLeft >= 5)
                {
                    bitsLeft -= 5;
                    int index = (buffer >> bitsLeft) & 0x1F;
                    sb.Append(Alphabet[index]);
                }
            }
            if (bitsLeft > 0)
            {
                int index = (buffer << (5 - bitsLeft)) & 0x1F;
                sb.Append(Alphabet[index]);
            }
            return sb.ToString();
        }

        private static string Group(string encoded)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < encoded.Length; i += 4)
            {
                if (i > 0) sb.Append('-');
                sb.Append(encoded, i, System.Math.Min(4, encoded.Length - i));
            }
            return sb.ToString();
        }
    }
}
