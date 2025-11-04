using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
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
            ?? throw new BicycleStoreException(ResourceMessagesException.BICYCLE_NO_FOUND);

        public async Task<Bicycle> GetBicycleByIdAsync(Guid id)
        {
            var bicycle = await _dbContext.Bicycles.FirstOrDefaultAsync(b => b.Id == id);

            if (bicycle == null)
                throw new BicycleStoreException(string.Format(ResourceMessagesException.BICYCLE_ID_NO_FOUND, id));

            return bicycle;
        }

        public async Task<IList<RentalOrder>> GetRentalOrdersAsync() => await _dbContext.RentalOrders.ToListAsync()
           ?? throw new BicycleStoreException(ResourceMessagesException.RENTAL_NO_FOUND);

        public async Task<RentalOrder> GetRentalOderByIdAsync(Guid id)
        {
            return await _dbContext.RentalOrders.FirstOrDefaultAsync(r => r.OrderId == id)
                   ?? throw new BicycleStoreException(string.Format(ResourceMessagesException.RENTAL_ID_NO_FOUND, id));
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

        public async Task UpdateRentalOrderAsync(RentalOrder rentalOrder)
        {
            _dbContext.RentalOrders.Update(rentalOrder);
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
