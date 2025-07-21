using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Domain.Repositories
{
    public interface IBicycleRepository
    {
        // DB
        public Task SaveChangesAsync();

        // GET
        public Task<IList<Bicycle>> GetBicyclesAsync();
        public Task<Bicycle> GetBicycleByIdAsync(Guid id);
        public Task<IList<RentalOrder>> GetRentalOrdersAsync();
        public Task<RentalOrder> GetRentalOderByIdAsync(Guid id);

        // POST
        public Task AddBicycleAsync(Bicycle bicycle);
        public Task AddRentalOrderAsync(RentalOrder rentalOrder);

        // PUT
        public Task UpdateBicycleAsync(Bicycle bicycle);
        public Task UpdateRentalOrderAsync(RentalOrder rentalOrder);

        // DELETE
        public Task DeleteBicycleAsync(Guid id);
        public Task DeleteRentalOrderAsync(Guid id);
    }
}
