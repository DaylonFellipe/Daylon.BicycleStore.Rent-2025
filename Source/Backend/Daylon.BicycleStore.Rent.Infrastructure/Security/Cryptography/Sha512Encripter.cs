using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
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
               ?? throw new BicycleStoreException(ResourceMessagesException.ENCRIPTER_NO_ADDICIONAL_KEY);
        }

        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new BicycleStoreException(ResourceMessagesException.USER_PASSWORD_CANNOT_BE_NULL_OR_EMPTY);

            var bytes = Encoding.UTF8.GetBytes($"{password}{_additionalKey}");
            var hash = SHA512.HashData(bytes);

            return StringBytes(hash);
        }
        public bool VerifyPassword(string providedPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(providedPassword))
                throw new BicycleStoreException(ResourceMessagesException.USER_PASSWORD_CANNOT_BE_NULL_OR_EMPTY);

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
