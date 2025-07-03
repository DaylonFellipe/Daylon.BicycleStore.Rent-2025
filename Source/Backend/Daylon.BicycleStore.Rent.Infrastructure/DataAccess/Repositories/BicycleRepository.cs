using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories
{
    public class BicycleRepository : IBicycleRepository
    {
        private readonly BicycleStoreDbContext _dbContext;

        public BicycleRepository(BicycleStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Bicycle>> GetBicyclesAsync() => await _dbContext.Bicycles.ToListAsync()
            ?? throw new Exception("No bike found.");

        public async Task<Bicycle> GetBicycleByIdAsync(Guid id)
        {
            return await _dbContext.Bicycles.FirstOrDefaultAsync(b => b.Id == id)
                   ?? throw new KeyNotFoundException($"Bicycle with ID {id} not found.");
        }
    }
}
