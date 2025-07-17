using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    public class RentalOrderUseCase : IRentalOrderUseCase
    {
        private readonly IBicycleRepository _bicycleRepository;

        public RentalOrderUseCase(IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }

        public async Task<Domain.Entity.RentalOrder> ExecuteRegisterRentalOrderAsync(RequestRegisterRentalOrderJson request, CancellationToken cancellationToken = default)
        {
            // Validate
            ValidateRequest(request, new RegisterRentalOrderValidator());

            // Map Properties
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(request.BicycleId);

            var rentalStart = DateTime.Now;
            var rentalEnd = rentalStart.AddDays(request.RentalDays);

            var totalPrice = bicycle.DailyRate * request.RentalDays;
            var orderStatus = OrderStatusEnum.Rented;

            // Create RentalOrder Entity
            var rentalOrder = new Domain.Entity.RentalOrder
            {
                OrderId = Guid.NewGuid(),

                RentalStart = rentalStart,
                RentalEnd = rentalEnd,
                RentalDays = request.RentalDays,
                DropOffTime = null,

                PaymentMethod = request.PaymentMethod,
                TotalPrice = totalPrice,
                OrderStatus = orderStatus,

                BicycleId = request.BicycleId,
                Bicycle = bicycle
            };

            //Save
            await _bicycleRepository.AddRentalOrderAsync(rentalOrder);

            return rentalOrder;
        }

        private static void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = validator.ValidateAsync(request);

            if (!result.Result.IsValid)
            {
                var erros = result.Result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", erros));
            }
        }
    }
}
