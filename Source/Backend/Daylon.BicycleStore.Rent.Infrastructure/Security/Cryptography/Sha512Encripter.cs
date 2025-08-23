using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography
{
    public class Sha512Encripter : IPasswordEncripter
    {
        private readonly string _additionalKey;

        public Sha512Encripter(string additionalKey)
        {
            _additionalKey = additionalKey;
        }

        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            var bytes = Encoding.UTF8.GetBytes($"{password}{_additionalKey}");
            var hash = SHA512.HashData(bytes);

            return StringBytes(hash);
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
