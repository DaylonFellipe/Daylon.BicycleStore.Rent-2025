using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IBicycleUseCase
    {
        public Task<Domain.Entity.Bicycle> ExecuteRegisterBicycleAsync(RequestRegisterBicycleJson request, CancellationToken cancellationToken = default);

        public Task<Domain.Entity.Bicycle> ExecuteUpdateBicycleAsync(RequestUpdateBicycleJson request, CancellationToken cancellationToken = default);

        public Task<Domain.Entity.Bicycle> ExecutePatchBicyclePartialAsync(
           Guid id,
           string? name,
           string? description,
           BrandEnum? brand,
           ModelEnum? model,
           ColorEnum? color,
           double? price,
           int? quantity,
           double? dailyRate,
           CancellationToken cancellationToken = default);
    }
}
