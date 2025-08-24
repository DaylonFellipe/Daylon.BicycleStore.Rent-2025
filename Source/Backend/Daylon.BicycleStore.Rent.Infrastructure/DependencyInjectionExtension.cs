using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories;
using Daylon.BicycleStore.Rent.Infrastructure.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Daylon.BicycleStore.Rent.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddServices(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            if (string.IsNullOrEmpty(connectionString))
            { throw new Exception("Connection string is not configured."); }

            services.AddDbContext<BicycleStoreDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBicycleRepository, BicycleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ISha512PasswordEncripter, Sha512Encripter>();
            services.AddScoped<IPBKDF2PasswordEncripter, PBKDF2Encripter>();
        }
    }
}
