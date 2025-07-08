using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            await _bicycleRepository.AddBicycleAsync(bicycle);

            return bicycle;
        }

        public async Task<Domain.Entity.Bicycle> ExecuteUpdateBicycleAsync(RequestUpdateBicycleJson request, CancellationToken cancellationToken = default)
        {
            // Get existing bicycle
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(request.Id);
            if (bicycle is null)
                throw new KeyNotFoundException($"Bicycle with ID {request.Id} not found.");

            // Validate
            var validator = new UpdateBicycleValidator();
            var result = validator.Validate(request);

            // Update properties
            if (!string.IsNullOrEmpty(request.Name)) bicycle.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description)) bicycle.Description = request.Description;

            if (request.Brand.HasValue && Enum.IsDefined(typeof(BrandEnum), request.Brand.Value))
                bicycle.Brand = request.Brand.Value;
            if (request.Model.HasValue && Enum.IsDefined(typeof(ModelEnum), request.Model.Value))
                bicycle.Model = request.Model.Value;
            if (request.Color.HasValue && Enum.IsDefined(typeof(ColorEnum), request.Color.Value))
                bicycle.Color = request.Color.Value;

            if (request.Price > 0) bicycle.Price = request.Price;
            if (request.Quantity >= 0) bicycle.Quantity = request.Quantity;
            bicycle.Status = request.Quantity > 0;

            // Save changes
            await _bicycleRepository.UpdateBicycleAsync(bicycle);

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
