using Daylon.BicycleStore.Rent.Communication.Request;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IRentalOrderUseCase
    {
        public Task<Domain.Entity.RentalOrder> ExecuteRegisterRentalOrderAsync(RequestRegisterRentalOrderJson request, CancellationToken cancellationToken = default);
    }
}
