using CommonTestUtilities.Requests.RentalOrder;
using Daylon.BicycleStore.Rent.Application.UseCases.Bicycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.RentalOrder
{
    public class RegisterRentalOrderTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // RENTAL DAYS

        [Fact]
        public void Error_Daily_Rate_Greater_Than_Zero()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.RentalDays = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_DAYS_GREATER_THAN_ZERO));
        }

        [Fact]
        public void Error_Daily_Rate_Max_Limit()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.RentalDays = 91;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_DAYS_MAX_LIMIT));
        }

        // PAYMENT METHOD

        [Fact]
        public void Error_Payment_Method_Required()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.PaymentMethod = (PaymentMethodEnum)999;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_PAYMENT_METHOD_REQUIRED));
        } 
        
        [Fact]
        public void Error_Payment_Method_Invalid_Enum()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.PaymentMethod = (PaymentMethodEnum)Enum.GetValues(typeof(PaymentMethodEnum)).Length + 1;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.RENTAL_PAYMENT_METHOD_INVALID_ENUM));
        }

        // BICYCLE ID

        [Fact]
        public void Error_Bicycle_Id_Empty()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.BicycleId = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_ID_EMPTY));
        }

        [Fact]
        public void Error_Bicycle_Id_Invalid()
        {
            var validator = new RegisterRentalOrderValidator();

            var request = RequestRegisterRentalOrderJsonBuilder.Build();
            request.BicycleId = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.BICYCLE_ID_INVALID));
        }
    }
}
