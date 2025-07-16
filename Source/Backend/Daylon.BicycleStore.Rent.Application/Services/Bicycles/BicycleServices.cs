using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class BicycleServices : IBicycleService
    {
        private readonly Domain.Repositories.IBicycleRepository _bicycleRepository;
        private readonly IBicycleUseCase _useCase;

        public BicycleServices(
            Domain.Repositories.IBicycleRepository bicycleRepository,
            IBicycleUseCase useCase)
        {
            _bicycleRepository = bicycleRepository;
            _useCase = useCase;
        }

        // GET
        public async Task<IList<Bicycle>> GetBicyclesAsync() => await _bicycleRepository.GetBicyclesAsync();

        public async Task<Bicycle> GetBicycleByIdAsync(Guid id)
        {
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(id);

            return bicycle;
        }

        // POST
        public async Task<Bicycle> RegisterBicycleAsync(RequestRegisterBicycleJson request)
        {
            var bicycle = await _useCase.ExecuteRegisterBicycleAsync(request);

            return bicycle;
        }

        // PUT
        public async Task<Bicycle> UpdateBicycleAsync(RequestUpdateBicycleJson request)
        {
            var bicycle = await _useCase.ExecuteUpdateBicycleAsync(request);

            return bicycle;
        }

        // PATCH    
        public async Task<Bicycle> PatchBicyclePartialAsync(
            Guid id,
            string? name,
            string? description,
            Domain.Entity.Enum.BrandEnum? brand,
            Domain.Entity.Enum.ModelEnum? model,
            Domain.Entity.Enum.ColorEnum? color,
            double? price,
            int? quantity,
            double? dailyRate
            )
        {
            var bicycle = await _useCase.ExecutePatchBicyclePartialAsync(
                id,
                name,
                description,
                brand,
                model,
                color,
                price,
                quantity,
                dailyRate
                );

            return bicycle;
        }

        // DELETE
        public async Task DeleteBicycleAsync(Guid id) => await _bicycleRepository.DeleteBicycleAsync(id);
    }
}
