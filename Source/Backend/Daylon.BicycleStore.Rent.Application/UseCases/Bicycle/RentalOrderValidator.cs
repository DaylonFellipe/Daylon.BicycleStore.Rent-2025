using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    internal static class RentalOrderValidator { }

    public class RegisterRentalOrderValidator : AbstractValidator<RequestRegisterRentalOrderJson>
    {
        public RegisterRentalOrderValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(r => r.RentalStart)
                .NotEmpty().WithMessage("Rental start date is required.")
                .Must(date => date > DateTime.MinValue).WithMessage("Rental start date must be a valid date.")
                .LessThan(r => r.RentalEnd).WithMessage("Rental start date must be before rental end date.");

            RuleFor(r => r.RentalEnd)
                .NotEmpty().WithMessage("Rental end date is required.")
                .Must(date => date > DateTime.MinValue).WithMessage("Rental start date must be a valid date.")
                .GreaterThan(r => r.RentalStart).WithMessage("Rental end date must be after rental start date.");

            RuleFor(r => r.RentalDays)
                .GreaterThan(0).WithMessage("Rental days must be greater than zero.")
                .LessThanOrEqualTo(30).WithMessage("Rental days cannot exceed 30 days.");

            RuleFor(r => r.DropOffTime)
                .NotEmpty().WithMessage("Drop-off time is required.")
                .Must(date => date > DateTime.MinValue).WithMessage("Drop-off time must be a valid date.")
                .GreaterThan(r => r.RentalEnd).WithMessage("Drop-off time must be after rental end date.");

            RuleFor(r => r.PaymentMethod)
                .IsInEnum().WithMessage("Payment method is required.")
                .Must(method => Enum.IsDefined(typeof(PaymentMethodEnum), method)).WithMessage("Payment method must be a valid enum value.");

            RuleFor(r => r.OrderStatus)
                .IsInEnum().WithMessage("Order status is required.")
                .Must(status => Enum.IsDefined(typeof(OrderStatusEnum), status)).WithMessage("Order status must be a valid enum value.");

            RuleFor(r => r.BicycleId)
                .NotEmpty().WithMessage("Bicycle ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Bicycle ID must be a valid GUID.");
        }
    }
}
