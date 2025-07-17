using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class RentalOrderService : IRentalOrderService
    {
        private readonly IRentalOrderUseCase _rentalOrderUseCase;

        public RentalOrderService(IRentalOrderUseCase rentalOrderUseCase)
        {
            _rentalOrderUseCase = rentalOrderUseCase;
        }

        // POST
        public async Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request)
        {
            var rentalOrder = await _rentalOrderUseCase.ExecuteRegisterRentalOrderAsync(request);

            return rentalOrder;
        }
    }
}
