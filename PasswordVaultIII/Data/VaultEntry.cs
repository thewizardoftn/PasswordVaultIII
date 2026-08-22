namespace PasswordVaultIII.Data
{
    public sealed class VaultEntry
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
