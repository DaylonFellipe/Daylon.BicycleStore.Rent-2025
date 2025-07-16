using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Application.Services.Bicycles;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Microsoft.Extensions.DependencyInjection;

namespace Daylon.BicycleStore.Rent.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IBicycleService, BicycleServices>();
            services.AddScoped<IBicycleUseCase, BicycleUseCase>();
            services.AddScoped<IRentalOrderService, RentalOrderService>();
            services.AddScoped<IRentalOrderUseCase, RentalOrderUseCase>();
        }
    }
}
