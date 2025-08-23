namespace Daylon.BicycleStore.Rent.Domain.Security.Cryptography
{
    public interface ISha512PasswordEncripter
    {
        string Encrypt(string password);
    }
}
