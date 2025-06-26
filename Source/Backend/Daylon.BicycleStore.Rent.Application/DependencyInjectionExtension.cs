using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Daylon.BicycleStore.Rent.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //AddServices(services);
        }

        //private static void AddServices(IServiceCollection services)
        //{
        //    services.AddScoped<IBicycleService, BicycleService>();
        //}
    }
}
