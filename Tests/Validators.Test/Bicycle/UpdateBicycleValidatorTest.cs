using CommonTestUtilities.Requests.Bicycle;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.Bicycle
{
    public class UpdateBicycleValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // NAME

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_NAME_EMPTY));
        }

        [Fact]
        public void Error_Name_Max_Lentgh()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Name = GenerateStringOfLength('a', 101);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_NAME_MAX_LENGTH));
        }

        // DESCRIPTION

        [Fact]
        public void Error_Description_Empty()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Description = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_DESCRIPTION_EMPTY));
        }

        [Fact]
        public void Error_Description_Max_Lentgh()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Description = GenerateStringOfLength('a', 501);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_DESCRIPTION_MAX_LENGTH));
        }

        // MODEL

        [Fact]
        public void Error_Model_Required()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Model = (ModelEnum)999;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_MODEL_REQUIRED));
        }

        [Fact]
        public void Error_Model_Ivalid_Enum()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Model = (ModelEnum)Enum.GetValues(typeof(ModelEnum)).Length + 1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM));
        }

        // BRAND       

        [Fact]
        public void Error_Brand_Required()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Brand = (BrandEnum)999;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_BRAND_REQUIRED));
        }

         [Fact]
        public void Error_Brand_Ivalid_Enum()
        {
            var validator = new RegisterBicycleValidator();

            var request = RequestRegisterBicycleJsonBuilder.Build();
            request.Brand = (BrandEnum)Enum.GetValues(typeof(BrandEnum)).Length + 1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_BRAND_INVALID_ENUM));
        }

        // COLOR    

        [Fact]
        public void Error_Color_Required()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Color = (ColorEnum)999;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_COLOR_REQUIRED));
        }

        [Fact]
        public void Error_Color_Ivalid_Enum()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Color = (ColorEnum)Enum.GetValues(typeof(ColorEnum)).Length + 1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_COLOR_INVALID_ENUM));
        }

        // PRICE

        [Fact]
        public void Error_Price_Greater_Than_Zero()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Price = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_PRICE_GREATER_THAN_ZERO));
        }

        // QUANTITY

        [Fact]
        public void Error_Quantity_Zero_Or_Greater()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.Quantity = -1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_QUANTITY_ZERO_OR_GREATER));
        }

        // DAILY RATE

        [Fact]
        public void Error_Daily_Rate_Greater_Than_Zero()
        {
            var validator = new UpdateBicycleValidator();

            var request = RequestUpdateBicycleJsonBuilder.Build();
            request.DailyRate = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_DAILY_RATE_GREATER_THAN_ZERO));
        }

        // AUXILIAR METHODS

        public string GenerateStringOfLength(char character, int length) => new string(character, length);
    }
}
