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

            RuleFor(r => r.RentalDays)
                .GreaterThan(0).WithMessage("Rental days must be greater than zero.")
                .LessThanOrEqualTo(90).WithMessage("Rental days cannot exceed 90 days.");

            RuleFor(r => r.PaymentMethod)
                .IsInEnum().WithMessage("Payment method is required.")
                .Must(method => Enum.IsDefined(typeof(PaymentMethodEnum), method)).WithMessage("Payment method must be a valid enum value.");

            RuleFor(r => r.BicycleId)
                .NotEmpty().WithMessage("Bicycle ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Bicycle ID must be a valid GUID.");
        }
    }

    public class ModifyDatesValidator : AbstractValidator<RequestModifyDatesValidatorJson>
    {
        public ModifyDatesValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(r => r.RentalStart)
                .Must(start => start == null || start >= DateTime.Now).WithMessage("Rental start date must be in the future or null.");

            //RuleFor(r => r.RentalEnd)
            //    .Must(end => end == null || end > DateTime.Now).WithMessage("Rental end date must be in the future or null.")
            //    .GreaterThanOrEqualTo(r => r.RentalStart).WithMessage("Rental end date must be after rental start date.");

            RuleFor(r => r.RentalDays)
                .Must(days => days == null || days > 0).WithMessage("Rental days must be greater than zero or null.")
                .GreaterThanOrEqualTo(1).WithMessage("Rental days must be at least 1 when both start and end dates are provided.");

            RuleFor(r => r.ExtraDays)
                .Must(extra => extra == null || extra >= 0).WithMessage("Extra days must be zero or greater.")
                .GreaterThanOrEqualTo(0).WithMessage("Extra days must be at least 0 when both start and end dates are provided.");
        }
    }
}