using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography
{
    public class PBKDF2Encripter : IPBKDF2PasswordEncripter
    {
        private const int Iterations = 100_000;
        private const int SaltSize = 16;
        private const int KeySize = 32;

        private static string? _pepper;

        public PBKDF2Encripter(IConfiguration configuration)
        {
            _pepper = configuration["Security:Pepper"]
                ?? throw new BicycleStoreException(ResourceMessagesException.ENCRIPTER_NO_PEPPER);
        }

        public string HashPassword_PBKDF2Encripter(string password)
        {
            // Generates random salt per user
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // combine password with pepper
            string combinedPassword = $"{password}{_pepper}";

            // Generates the key using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password: combinedPassword,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256);

            byte[] key = pbkdf2.GetBytes(KeySize);

            // Returns in secure format (version$iter$salt$hashKey)
            return string.Join('$',
                "v1",
                Iterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(key));
        }

        public bool VerifyPassword_PBKDF2Encripter(string providedPassword, string storedHash)
        {
            // Split the hash into parts
            var parts = storedHash.Split('$');

            if (parts.Length != 4 || parts[0] != "v1")
                throw new BicycleStoreException(ResourceMessagesException.ENCRIPTER_HASH_INVALID_FORMAT);

            // Extract the number of iterations, salt and hash
            int iterations = int.Parse(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] storedKey = Convert.FromBase64String(parts[3]);

            // Combine the provided password with the pepper
            string combinedPassword = $"{providedPassword}{_pepper}";

            // Generate the key using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password: combinedPassword,
                salt: salt,
                iterations: iterations,
                hashAlgorithm: HashAlgorithmName.SHA256);

            byte[] computeredKey = pbkdf2.GetBytes(KeySize);

            // VALIDATION - Compares the computed key with the stored key
            return computeredKey.SequenceEqual(storedKey);
        }
    }
}