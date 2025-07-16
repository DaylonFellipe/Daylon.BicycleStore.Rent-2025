using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    public class RentalOrderUseCase : IRentalOrderUseCase
    {
        // private readonly Interface nomeDaInterface;

        public RentalOrderUseCase()
        {

        }

        public async Task<Domain.Entity.RentalOrder> ExecuteRegisterRentalOrderAsync(RequestRegisterRentalOrderJson request, CancellationToken cancellationToken = default)
        {
            // Validate


            // Map
            var rentalOrder = new Domain.Entity.RentalOrder
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                BicycleId = request.BicycleId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalPrice = CalculateTotalPrice(request.StartDate, request.EndDate, request.DailyRate)
            };

            // Save

            // await _rentalOrderRepository.AddRentalOrderAsync(rentalOrder);
            return rentalOrder;



        }

        private static void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = validator.ValidateAsync(request);

            if (!result.Result.IsValid)
            {
                var erros = result.Result.Errors.Select(e => e.ErrorMessage).ToList();
            }
        }
    }
}
