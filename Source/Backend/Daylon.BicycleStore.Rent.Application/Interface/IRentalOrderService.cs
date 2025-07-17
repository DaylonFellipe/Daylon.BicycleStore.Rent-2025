using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IRentalOrderService
    {
        public Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request);
    }
}
