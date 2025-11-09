using CommonTestUtilities.Requests.RentalOrder;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.RentalOrder
{
    public class ModifyDatesValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // RENTAL START

        [Fact]
        public void Success_Rental_Start_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.RentalStart = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Rental_Start_In_Future_Or_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.RentalStart = DateTime.Now.AddDays(-1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_START_IN_FUTURE_OR_NULL));
        }

        // RENTAL DAYS

        [Fact]
        public void Success_Rental_Days_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.RentalDays = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Rental_Days_Greater_Than_Zero_Or_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.RentalDays = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_DAYS_GREATER_THAN_ZERO_OR_NULL));
        }

        [Fact]
        public void Error_Rental_Days_Minimum_One()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.RentalDays = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_DAYS_MINIMUM_ONE));
        }

        // EXTRA DAYS

        [Fact]
        public void Success_Extra_Days_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.ExtraDays = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Extra_Days_Greater_Than_Zero_Or_Null()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.ExtraDays = -1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_EXTRA_DAYS_MINIMUM_ZERO));
        }

        [Fact]
        public void Error_Extra_Days_Minimum_One()
        {
            var validator = new ModifyDatesValidator();

            var request = RequestModifyDatesValidatorJsonBuilder.Build();
            request.ExtraDays = -1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_EXTRA_DAYS_ZERO_OR_GREATER));
        }
    }
}