using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
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
            await ModifyOrderStatusToOverdueAsync();

            var rentalOrders = await _bicycleRepository.GetRentalOrdersAsync();

            return rentalOrders;
        }

        public async Task<RentalOrder> GetRentalOrderByIdAsync(Guid id)
        {
            await ModifyOrderStatusToOverdueAsync();

            var rentalOrder = await _bicycleRepository.GetRentalOderByIdAsync(id);

            return rentalOrder;
        }

        // POST
        public async Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request)
        {
            var rentalOrder = await _rentalOrderUseCase.ExecuteRegisterRentalOrderAsync(request);

            return rentalOrder;
        }

        // PATCH
        public async Task<RentalOrder> ModifyDatesAsync(Guid id, DateTime? rentalStart, int? rentalDays, int? extraDays)
        {
            var rentalOrder = await _bicycleRepository.GetRentalOderByIdAsync(id);

            if (rentalOrder == null)
                throw new KeyNotFoundException($"Rental order with ID {id} not found.");

            var updatedRentalOrder = await _rentalOrderUseCase.ExecuteModifyDatesAsync(id, rentalStart, rentalDays, extraDays);

            await ModifyOrderStatusToOverdueAsync();

            return updatedRentalOrder;
        }

        public async Task ModifyOrderStatusToOverdueAsync()
        {
            var OrderList = await _bicycleRepository.GetRentalOrdersAsync();

            foreach (var order in OrderList)
            {
                if (order.OrderStatus == OrderStatusEnum.Rented && order.RentalEnd < DateTime.Now)
                    order.OrderStatus = OrderStatusEnum.Overdue;

                await _bicycleRepository.UpdateRentalOrderAsync(order);
            }
        }

        // DELETE
        public async Task DeleteRentalOrderAsync(Guid id) => await _bicycleRepository.DeleteRentalOrderAsync(id);
    }
}
