using Daylon.BicycleStore.Rent.Communication.Request;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    public class BicycleUseCase
    {








        public async Task<Domain.Entity.Bicycle> ExecuteRegisterBicycleAsync(RequestRegisterBicycleJson request, CancellationToken cancellationToken = default)
        {
            // Validate
            ValidateRequest(request, new RegisterBicycleValidator());


            // Map

            // Save

        }

        private static void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var resutl = validator.ValidateAsync(request);

            if (!resutl.Result.IsValid)
            { var erros = resutl.Result.Errors.Select(e => e.ErrorMessage).ToList(); }

        }
    }
}
