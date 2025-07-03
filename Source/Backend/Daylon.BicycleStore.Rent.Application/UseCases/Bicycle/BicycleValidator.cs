using Daylon.BicycleStore.Rent.Communication.Request;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    internal static class BicycleValidator{ }

    public class RegisterBicycleValidator : AbstractValidator<RequestRegisterBicycleJson>
    {
        public RegisterBicycleValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Bicycle name is required.")
                .MaximumLength(100).WithMessage("Bicycle name must not exceed 100 characters.");
            
            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Bicycle name is required.")
                .MaximumLength(500).WithMessage("Bicycle name must not exceed 500 characters.");

            RuleFor(m => m.Model)
                .IsInEnum().WithMessage("Valid model is required");

            RuleFor(b => b.Brand)
                .IsInEnum().WithMessage("Valid brand is required");

            RuleFor(c => c.Color)
                .IsInEnum().WithMessage("Valid color is required");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(q => q.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be zero or greater.");
        }
    }
}
