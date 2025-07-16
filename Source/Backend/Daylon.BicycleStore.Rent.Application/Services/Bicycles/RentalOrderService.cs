using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class RentalOrderService : IRentalOrderService
    {
        // private readonly Interface nomeDaInterface;

        public RentalOrderService()
        {

        }

        public async Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request)
        {
            var rentalOrder = await UseCases

            return rentalOrder;
        }
    }
}
