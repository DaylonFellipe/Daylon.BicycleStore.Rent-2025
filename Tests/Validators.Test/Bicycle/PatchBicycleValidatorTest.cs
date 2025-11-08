using CommonTestUtilities.Requests.Bicycle;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.Bicycle
{
    public class PatchBicycleValidatorTest
    {
        [Fact]
        public async void Success()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        // NAME

        [Fact]
        public async void Success_Name_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Name = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Name_Max_Lenght()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Name = GenerateStringOfLength('a', 101);

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_NAME_MAX_LENGTH));
        }

        // DESCRIPTION

        [Fact]
        public async void Success_Description_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Description = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Description_Max_Lenght()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Description = GenerateStringOfLength('a', 501);

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_DESCRIPTION_MAX_LENGTH));
        }

        // MODEL    

        [Fact]
        public async void Success_Model_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Model = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Model_Ivalid_Enum()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Model = (ModelEnum)Enum.GetValues(typeof(ModelEnum)).Length + 1;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_MODEL_INVALID_ENUM));
        }

        // BRAND       

        [Fact]
        public async void Success_Brand_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Brand = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Brand_Ivalid_Enum()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Brand = (BrandEnum)Enum.GetValues(typeof(BrandEnum)).Length + 1;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_BRAND_INVALID_ENUM));
        }

        // COLOR    

        [Fact]
        public async void Success_Color_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Color = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Color_Ivalid_Enum()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Color = (ColorEnum)Enum.GetValues(typeof(ColorEnum)).Length + 1;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_COLOR_INVALID_ENUM));
        }

        // PRICE

        [Fact]
        public async void Success_Price_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Price = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Price_Greater_Than_Zero()
        {
            var validator = new RegisterBicycleValidator();

            var request = RequestRegisterBicycleJsonBuilder.Build();
            request.Price = 0;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_PRICE_GREATER_THAN_ZERO));
        }

        // QUANTITY

        [Fact]
        public async void Success_Quantity_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.Quantity = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Quantity_Greater_Than_Zero()
        {
            var validator = new RegisterBicycleValidator();

            var request = RequestRegisterBicycleJsonBuilder.Build();
            request.Quantity = -1;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_QUANTITY_ZERO_OR_GREATER));
        }

        
        // DAILY RATE  

        [Fact]
        public async void Success_Daily_Rate_Null()
        {
            var validator = new PatchBicycleValidator();

            var request = RequestPatchBicycleValidatorJsonBuilder.Build();
            request.DailyRate = null;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async void Error_Daily_Rate_Greater_Than_Zero()
        {
            var validator = new RegisterBicycleValidator();

            var request = RequestRegisterBicycleJsonBuilder.Build();
            request.DailyRate = 0;

            var result = await validator.ValidateAsync(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_DAILY_RATE_GREATER_THAN_ZERO));
        }

        // AUXILIAR METHODS

        public string GenerateStringOfLength(char character, int length) => new string(character, length);
    }
}
