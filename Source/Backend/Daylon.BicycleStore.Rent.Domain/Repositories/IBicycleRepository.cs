using Daylon.BicycleStore.Rent.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Daylon.BicycleStore.Rent.Domain.Repositories
{
    public interface IBicycleRepository
    {
        // DB
        public Task SaveChangesAsync();

        // GET
        public Task<IList<Bicycle>> GetBicyclesAsync();

        public Task<Bicycle> GetBicycleByIdAsync(Guid id);

        // POST
        public Task AddBicycleAsync(Bicycle bicycle);

        // PUT
        public Task UpdateBicycleAsync(Bicycle bicycle);

        // DELETE
        public Task DeleteBicycleAsync(Guid id);
    }
}
