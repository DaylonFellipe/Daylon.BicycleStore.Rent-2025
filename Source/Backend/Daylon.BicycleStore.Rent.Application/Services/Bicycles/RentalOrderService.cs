using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Repositories;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class RentalOrderService : IRentalOrderService
    {
        private readonly IRentalOrderUseCase _rentalOrderUseCase;
        private readonly IBicycleRepository _bicycleRepository;

        public RentalOrderService(
            IRentalOrderUseCase rentalOrderUseCase,
             IBicycleRepository bicycleRepository)
        {
            _rentalOrderUseCase = rentalOrderUseCase;
            _bicycleRepository = bicycleRepository;
        }

        // GET
        public async Task<IList<RentalOrder>> GetRentalOrdersAsync()
        {
            var rentalOrders = await _bicycleRepository.GetRentalOrdersAsync();

            return rentalOrders;
        }

        public async Task<RentalOrder> GetRentalOrderByIdAsync(Guid id)
        {
            var rentalOrder = await _bicycleRepository.GetRentalOderById(id);

            return rentalOrder;
        }

        // POST
        public async Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request)
        {
            var rentalOrder = await _rentalOrderUseCase.ExecuteRegisterRentalOrderAsync(request);

            return rentalOrder;
        }
    }
}
