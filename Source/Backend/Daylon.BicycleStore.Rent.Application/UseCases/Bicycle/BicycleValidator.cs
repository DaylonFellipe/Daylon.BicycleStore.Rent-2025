using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    internal static class BicycleValidator { }

    public class RegisterBicycleValidator : AbstractValidator<RequestRegisterBicycleJson>
    {
        public RegisterBicycleValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(n => n.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.BICYCLE_NAME_EMPTY)
                .MaximumLength(100).WithMessage(ResourceMessagesException.BICYCLE_NAME_MAX_LENGTH);

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage(ResourceMessagesException.BICYCLE_DESCRIPTION_EMPTY)
                .MaximumLength(500).WithMessage(ResourceMessagesException.BICYCLE_DESCRIPTION_MAX_LENGTH);

            RuleFor(m => m.Model)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_MODEL_REQUIRED)
                .Must(model => Enum.IsDefined(typeof(ModelEnum), model)).WithMessage(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM);

            RuleFor(b => b.Brand)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_BRAND_REQUIRED)
                .Must(brand => Enum.IsDefined(typeof(BrandEnum), brand)).WithMessage(ResourceMessagesException.BICYCLE_BRAND_INVALID_ENUM);

            RuleFor(c => c.Color)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_COLOR_REQUIRED)
                .Must(color => Enum.IsDefined(typeof(ColorEnum), color)).WithMessage(ResourceMessagesException.BICYCLE_COLOR_INVALID_ENUM);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage(ResourceMessagesException.BICYCLE_PRICE_GREATER_THAN_ZERO);

            RuleFor(q => q.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.BICYCLE_QUANTITY_ZERO_OR_GREATER);

            RuleFor(d => d.DailyRate)
                .GreaterThan(0).WithMessage(ResourceMessagesException.BICYCLE_DAILY_RATE_GREATER_THAN_ZERO);
        }
    }

    public class UpdateBicycleValidator : AbstractValidator<RequestUpdateBicycleJson>
    {
        public UpdateBicycleValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(n => n.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.BICYCLE_NAME_EMPTY)
                .MaximumLength(100).WithMessage(ResourceMessagesException.BICYCLE_NAME_MAX_LENGTH);

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Bicycle name is required.")
                .MaximumLength(500).WithMessage("Bicycle name must not exceed 500 characters.");

            RuleFor(m => m.Model)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_MODEL_REQUIRED)
                .Must(model => model is not null && Enum.IsDefined(typeof(ModelEnum), model)).WithMessage(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM);

            RuleFor(b => b.Brand)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_BRAND_REQUIRED)
                .Must(brand => brand is not null && Enum.IsDefined(typeof(BrandEnum), brand)).WithMessage(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM);

            RuleFor(c => c.Color)
                .IsInEnum().WithMessage(ResourceMessagesException.BICYCLE_COLOR_REQUIRED)
                .Must(color => color is not null && Enum.IsDefined(typeof(ColorEnum), color)).WithMessage(ResourceMessagesException.BICYCLE_COLOR_INVALID_ENUM);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage(ResourceMessagesException.BICYCLE_PRICE_GREATER_THAN_ZERO);

            RuleFor(q => q.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.BICYCLE_QUANTITY_ZERO_OR_GREATER);

            RuleFor(d => d.DailyRate)
                .GreaterThan(0).WithMessage(ResourceMessagesException.BICYCLE_DAILY_RATE_GREATER_THAN_ZERO);      
        }
    }

    public class PatchBicycleValidator : AbstractValidator<RequestPatchBicycleJson>
    {
        public PatchBicycleValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(n => n.Name).MustAsync(async (name, cancellation) =>
            {
                if (string.IsNullOrEmpty(name)) return true;
                return name.Length <= 100;
            }).MaximumLength(100).WithMessage("Bicycle name must not exceed 100 characters.");

            RuleFor(d => d.Description).MustAsync(async (description, cancellation) =>
            {
                if (string.IsNullOrEmpty(description)) return true;
                return description.Length <= 500;
            }).MaximumLength(500).WithMessage("Bicycle descrption must not exceed 500 characters.");

            RuleFor(m => m.Model)
                .IsInEnum().WithMessage("Valid model is required")
                .Must(model => model is null || Enum.IsDefined(typeof(ModelEnum), model)).WithMessage("Model must be a valid enum value.");

            RuleFor(b => b.Brand)
                .IsInEnum().WithMessage("Valid brand is required")
                .Must(brand => brand is null || Enum.IsDefined(typeof(BrandEnum), brand)).WithMessage("Brand must be a valid enum value.");

            RuleFor(c => c.Color)
                .IsInEnum().WithMessage("Valid color is required")
                .Must(color => color is null || Enum.IsDefined(typeof(ColorEnum), color)).WithMessage("Color must be a valid enum value.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(q => q.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be zero or greater.");

            RuleFor(d => d.DailyRate)
                .GreaterThan(0).WithMessage("Daily rate must be greater than zero.");
        }
    }
}
