using CommonTestUtilities.Requests;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using FluentAssertions;

namespace Validators.Test.User
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.FirstName = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }

   
    }
}
