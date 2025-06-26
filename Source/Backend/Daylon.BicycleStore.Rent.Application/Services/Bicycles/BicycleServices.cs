using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class BicycleServices : IBicycleService
    {
        private readonly Domain.Repositories.IBicycleRepository _bicycleRepository;

        public BicycleServices(Domain.Repositories.IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }

        public async Task<IList<Bicycle>> GetBicyclesAsync() => await _bicycleRepository.GetBicyclesAsync();
    }
}
