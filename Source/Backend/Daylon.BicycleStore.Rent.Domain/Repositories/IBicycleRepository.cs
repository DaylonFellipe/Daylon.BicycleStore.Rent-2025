using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Domain.Repositories
{
    public interface IBicycleRepository
    {
        public Task<IList<Bicycle>> GetBicyclesAsync();

        public Task<Bicycle> GetBicycleByIdAsync(Guid id);
    }
}
