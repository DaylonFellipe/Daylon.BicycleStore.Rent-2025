using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Application.Services.Bicycles;
using Daylon.BicycleStore.Rent.Application.Services.User;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserUseCase, UserUseCase>();
        }
    }
}
