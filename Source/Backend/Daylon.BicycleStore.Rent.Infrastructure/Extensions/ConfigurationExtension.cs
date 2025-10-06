using Microsoft.Extensions.Configuration;

namespace Daylon.BicycleStore.Rent.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnvironment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
    }
}
