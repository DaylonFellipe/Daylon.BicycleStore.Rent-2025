using Daylon.BicycleStore.Rent.Communication.Request;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IBicycleUseCase
    {
        public Task<Domain.Entity.Bicycle> ExecuteRegisterBicycleAsync(RequestRegisterBicycleJson request, CancellationToken cancellationToken = default);

        public Task<Domain.Entity.Bicycle> ExecuteUpdateBicycleAsync(RequestUpdateBicycleJson request, CancellationToken cancellationToken = default);
    }
}
