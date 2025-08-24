using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IBicycleService
    {
        // GET
        public Task<IList<Bicycle>> GetBicyclesAsync();

        public Task<Bicycle> GetBicycleByIdAsync(Guid id);

        // POST
        public Task<Bicycle> RegisterBicycleAsync(RequestRegisterBicycleJson request);

        // PUT
        public Task<Bicycle> UpdateBicycleAsync(RequestUpdateBicycleJson request);

        // PATCH
        public Task<Bicycle> PatchBicyclePartialAsync(
           Guid id,
           string? name,
           string? description,
           Domain.Entity.Enum.BrandEnum? brand,
           Domain.Entity.Enum.ModelEnum? model,
           Domain.Entity.Enum.ColorEnum? color,
           double? price,
           int? quantity,
           double? dailyRate
          );

        // DELETE
        public Task DeleteBicycleAsync(Guid id);
    }
}
