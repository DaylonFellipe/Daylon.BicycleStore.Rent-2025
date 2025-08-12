using Daylon.BicycleStore.Rent.Communication.Request;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IRentalOrderUseCase
    {
        // POST
        public Task<Domain.Entity.RentalOrder> ExecuteRegisterRentalOrderAsync(RequestRegisterRentalOrderJson request, CancellationToken cancellationToken = default);

        // PATCH
        public Task<Domain.Entity.RentalOrder> ExecuteModifyDatesAsync(Guid id, DateTime? rentalStart, int? rentalDays, int? extraDays);
    }
}
