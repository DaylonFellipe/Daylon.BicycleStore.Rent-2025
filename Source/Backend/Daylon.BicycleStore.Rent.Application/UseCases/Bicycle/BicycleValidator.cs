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
                .NotEmpty().WithMessage(ResourceMessagesException.BICYCLE_DESCRIPTION_EMPTY)
                .MaximumLength(500).WithMessage(ResourceMessagesException.BICYCLE_DESCRIPTION_MAX_LENGTH);

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

            RuleFor(n => n.Name).MustAsync((name, cancellation) =>
            {
                if (string.IsNullOrEmpty(name)) return Task.FromResult(true);
                return Task.FromResult(name.Length <= 100);
            }).MaximumLength(100).WithMessage(ResourceMessagesException.BICYCLE_NAME_MAX_LENGTH);

            RuleFor(d => d.Description).MustAsync((description, cancellation) =>
            {
                if (string.IsNullOrEmpty(description)) return Task.FromResult(true);
                return Task.FromResult(description.Length <= 500);
            }).MaximumLength(500).WithMessage(ResourceMessagesException.BICYCLE_DESCRIPTION_MAX_LENGTH);

            RuleFor(m => m.Model).MustAsync((model, cancellation) =>
            {
                if (model.HasValue is false || model is null) return Task.FromResult(true);
                return Task.FromResult(Enum.IsDefined(typeof(ModelEnum), model));
            }).WithMessage(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM);

            RuleFor(b => b.Brand).MustAsync((brand, cancellation) =>
            {
                if (brand.HasValue is false || brand is null) return Task.FromResult(true);
                return Task.FromResult(Enum.IsDefined(typeof(BrandEnum), brand));
            }).WithMessage(ResourceMessagesException.BICYCLE_BRAND_INVALID_ENUM);

            RuleFor(c => c.Color).MustAsync((color, cancellation) =>
            {
                if (color.HasValue is false || color is null) return Task.FromResult(true);
                return Task.FromResult(Enum.IsDefined(typeof(ColorEnum), color));
            }).WithMessage(ResourceMessagesException.BICYCLE_COLOR_INVALID_ENUM);

            RuleFor(p => p.Price)
                 .Must(price => price == null || price > 0)
                 .WithMessage(ResourceMessagesException.BICYCLE_PRICE_GREATER_THAN_ZERO);

            RuleFor(q => q.Quantity)
                .Must(quantity => quantity == null || quantity >= 0)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.BICYCLE_QUANTITY_ZERO_OR_GREATER);

            RuleFor(d => d.DailyRate)
                .Must(dailyRate => dailyRate == null || dailyRate > 0)
                .GreaterThan(0).WithMessage(ResourceMessagesException.BICYCLE_DAILY_RATE_GREATER_THAN_ZERO);
        }
    }
}