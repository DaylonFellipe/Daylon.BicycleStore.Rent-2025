using Daylon.BicycleStore.Rent.Communication.Request;
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

        // DELETE
        public Task DeleteBicycleAsync(Guid id);
    }
}
