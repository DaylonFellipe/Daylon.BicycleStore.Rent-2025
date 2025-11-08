using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Exceptions;
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
                .GreaterThan(0).WithMessage(ResourceMessagesException.RENTAL_DAYS_GREATER_THAN_ZERO)
                .LessThanOrEqualTo(90).WithMessage(ResourceMessagesException.RENTAL_DAYS_MAX_LIMIT);

            RuleFor(r => r.PaymentMethod)
                .IsInEnum().WithMessage(ResourceMessagesException.RENTAL_PAYMENT_METHOD_REQUIRED)
                .Must(method => Enum.IsDefined(typeof(PaymentMethodEnum), method)).WithMessage(ResourceMessagesException.RENTAL_PAYMENT_METHOD_INVALID_ENUM);

            RuleFor(r => r.BicycleId)
                .NotEmpty().WithMessage(ResourceMessagesException.BICYCLE_ID_EMPTY)
                .Must(id => id != Guid.Empty).WithMessage(ResourceMessagesException.BICYCLE_ID_INVALID);
        }
    }

    public class ModifyDatesValidator : AbstractValidator<RequestModifyDatesValidatorJson>
    {
        public ModifyDatesValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(r => r.RentalStart)
                .Must(start => start == null || start >= DateTime.Now).WithMessage(ResourceMessagesException.RENTAL_START_IN_FUTURE_OR_NULL);

            //RuleFor(r => r.RentalEnd)
            //    .Must(end => end == null || end > DateTime.Now).WithMessage("Rental end date must be in the future or null.")
            //    .GreaterThanOrEqualTo(r => r.RentalStart).WithMessage("Rental end date must be after rental start date.");

            RuleFor(r => r.RentalDays)
                .Must(days => days == null || days > 0).WithMessage(ResourceMessagesException.RENTAL_DAYS_GREATER_THAN_ZERO_OR_NULL)
                .GreaterThanOrEqualTo(1).WithMessage(ResourceMessagesException.RENTAL_DAYS_MINIMUM_ONE);

            RuleFor(r => r.ExtraDays)
                .Must(extra => extra == null || extra >= 0).WithMessage(ResourceMessagesException.RENTAL_EXTRA_DAYS_MINIMUM_ZERO)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.RENTAL_EXTRA_DAYS_ZERO_OR_GREATER);
        }
    }
}