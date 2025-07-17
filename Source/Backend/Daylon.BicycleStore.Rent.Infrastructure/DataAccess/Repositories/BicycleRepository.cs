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

        // DB
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        // GET 
        public async Task<IList<Bicycle>> GetBicyclesAsync() => await _dbContext.Bicycles.ToListAsync()
            ?? throw new Exception("No bike found.");

        public async Task<Bicycle> GetBicycleByIdAsync(Guid id)
        {
            return await _dbContext.Bicycles.FirstOrDefaultAsync(b => b.Id == id)
                   ?? throw new KeyNotFoundException($"Bicycle with ID {id} not found.");
        }

        public async Task<IList<RentalOrder>> GetRentalOrdersAsync() => await _dbContext.RentalOrders.ToListAsync()
           ?? throw new Exception("No rental order found.");

        public async Task<RentalOrder> GetRentalOderByIdAsync(Guid id)
        {
            return await _dbContext.RentalOrders.FirstOrDefaultAsync(r => r.OrderId == id)
                   ?? throw new KeyNotFoundException($"Rental order with ID {id} not found.");
        }

        // POST
        public async Task AddBicycleAsync(Bicycle bicycle)
        {
            await _dbContext.Bicycles.AddAsync(bicycle);
            await SaveChangesAsync();
        }

        public async Task AddRentalOrderAsync(RentalOrder rentalOrder)
        {
            await _dbContext.RentalOrders.AddAsync(rentalOrder);
            await SaveChangesAsync();
        }

        //PUT
        public async Task UpdateBicycleAsync(Bicycle bicycle)
        {
            _dbContext.Bicycles.Update(bicycle);
            await SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteBicycleAsync(Guid id)
        {
            var bicycle = await GetBicycleByIdAsync(id);
            _dbContext.Bicycles.Remove(bicycle);
            await SaveChangesAsync();
        }

        public async Task DeleteRentalOrderAsync(Guid id)
        {
            var rentalOrder = await GetRentalOderByIdAsync(id);
            _dbContext.RentalOrders.Remove(rentalOrder);
            await SaveChangesAsync();
        }
    }
}
