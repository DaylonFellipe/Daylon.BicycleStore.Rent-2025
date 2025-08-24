using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography
{
    public class Sha512Encripter : ISha512PasswordEncripter
    {
        private static string? _additionalKey;

        public Sha512Encripter(IConfiguration configuration)
        {
            _additionalKey = configuration["Security:AdditionalKey"]
               ?? throw new InvalidOperationException("AdditionalKey configuration value is missing.");
        }

        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            var bytes = Encoding.UTF8.GetBytes($"{password}{_additionalKey}");
            var hash = SHA512.HashData(bytes);

            return StringBytes(hash);
        }
        public bool VerifyPassword(string providedPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(providedPassword))
                throw new ArgumentException("Password cannot be null or empty.", nameof(providedPassword));

            var bytes = Encoding.UTF8.GetBytes($"{providedPassword}{_additionalKey}");
            var computedHash = SHA512.HashData(bytes);

            return storedHash.Equals(StringBytes(computedHash));
        }

        private static string StringBytes(byte[] bytes)
        {
            var stringBuilder = new StringBuilder();

            foreach (var b in bytes)
                stringBuilder.Append(b.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}
