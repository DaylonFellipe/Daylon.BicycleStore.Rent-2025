using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IBicycleService
    {
        public Task<IList<Bicycle>> GetBicyclesAsync();

        public Task<Bicycle> GetBicycleByIdAsync(Guid id);
    }
}
