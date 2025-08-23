namespace Daylon.BicycleStore.Rent.Domain.Security.Cryptography
{
    public interface IPBKDF2PasswordEncripter
    {
        public string HashPassword_PBKDF2Encripter(string password);

        public bool VerifyPassword_PBKDF2Encripter(string providedPassword, string storedHash);
    }
}
