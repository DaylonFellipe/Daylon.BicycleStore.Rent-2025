using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    public class BicycleUseCase : IBicycleUseCase
    {
        private readonly IBicycleRepository _bicycleRepository;

        public BicycleUseCase(IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }

        public async Task<Domain.Entity.Bicycle> ExecuteRegisterBicycleAsync(RequestRegisterBicycleJson request, CancellationToken cancellationToken = default)
        {
            // Validate
            ValidateRequest(request, new RegisterBicycleValidator());

            // Map
            var bicycle = new Domain.Entity.Bicycle
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                Model = request.Model,
                Color = request.Color,
                Price = request.Price,
                Quantity = request.Quantity,
                Status = request.Quantity > 0 
            };

            // Save
            await _bicycleRepository.AddAsync(bicycle);

            return bicycle;
        }

        private static void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var resutl = validator.ValidateAsync(request);

            if (!resutl.Result.IsValid)
            { var erros = resutl.Result.Errors.Select(e => e.ErrorMessage).ToList(); }

        }
    }
}
