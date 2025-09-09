namespace URLShortener.Utils
{
    using System.Security.Cryptography;
    using System.Text;

    public static class HashHelper
    {
        public static string GenerateHash(string input, int length = 8)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes)
                .Replace("+", "")
                .Replace("/", "")
                .Replace("=", "")
                .Substring(0, length);
        }
    }
}
