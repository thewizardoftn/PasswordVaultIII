using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace PasswordVaultIII.Data
{
    // Calls into the original PasswordVaultII app's compiled encryption library
    // (clsPWV.dll) via reflection, so the real legacy passwords can be recovered
    // during import instead of left blank. Its source was lost long ago, but the
    // compiled binary still works fine when loaded directly - it's a plain library
    // with no dependency on the old .NET Framework runtime it was built for.
    // Purely best-effort: if the DLL isn't sitting next to the exe, or anything
    // about it doesn't match what's expected, callers just get "not available"
    // and fall back to leaving passwords blank, same as before this existed.
    public sealed class LegacyPasswordDecryptor
    {
        private readonly object _instance;
        private readonly MethodInfo _decryptMethod;

        static LegacyPasswordDecryptor()
        {
            // clsPWV was built against .NET Framework, where every legacy Windows codepage
            // (including 1252, which it uses internally) is available out of the box. .NET 8
            // only ships Unicode encodings unless this provider is registered explicitly.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private LegacyPasswordDecryptor(object instance, MethodInfo decryptMethod)
        {
            _instance = instance;
            _decryptMethod = decryptMethod;
        }

        public static LegacyPasswordDecryptor TryCreate()
        {
            try
            {
                string dllPath = Path.Combine(AppContext.BaseDirectory, "clsPWV.dll");
                if (!File.Exists(dllPath)) return null;

                Assembly assembly = Assembly.LoadFrom(dllPath);
                Type type = assembly.GetType("clsPWV.clsPWV");
                if (type == null) return null;

                MethodInfo decryptMethod = type.GetMethod("Decrypt", new[] { typeof(string) });
                if (decryptMethod == null) return null;

                object instance = Activator.CreateInstance(type);
                return new LegacyPasswordDecryptor(instance, decryptMethod);
            }
            catch
            {
                return null;
            }
        }

        public string Decrypt(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted)) return string.Empty;
            return (string)_decryptMethod.Invoke(_instance, new object[] { encrypted });
        }
    }
}
