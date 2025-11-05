using Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace CommonTestUtilities.Cryptography
{
    public class PBKDF2EncripterBuilder
    {
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
    }
}
