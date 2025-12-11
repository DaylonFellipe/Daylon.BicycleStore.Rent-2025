using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PBKDF2EncripterBuilder
    {
        private readonly Mock<IPBKDF2PasswordEncripter> _encripter;

        public PBKDF2EncripterBuilder() =>
            _encripter = new Mock<IPBKDF2PasswordEncripter>();

        public static PBKDF2Encripter Build()
        {
            //return new PBKDF2Encripter(configuration);
            var pepper = "TestPepperValue";

            return new PBKDF2Encripter(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Security:Pepper", pepper }
                })
                .Build());
        }

        public IPBKDF2PasswordEncripter BuildMock() => _encripter.Object;
    }
}
