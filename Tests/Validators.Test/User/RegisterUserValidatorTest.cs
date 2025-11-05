using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Threading.Tasks;

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

        // FIRST NAME
        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.FirstName = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_EMPTY));
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_INVALID_CHARACTERS));
        }

        [Fact]
        public void Error_Name_Max_Lentgh()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.FirstName = GenerateStringOfLength('a', 101);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_MAX_LENGTH));
        }

        [Fact]
        public void Error_Name_Invalid_Characters()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.FirstName = GenerateStringOfLength('1', 10) + "123@#";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_INVALID_CHARACTERS));
        }

        // LAST NAME
        [Fact]
        public void Error_Last_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.LastName = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_EMPTY));
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_INVALID_CHARACTERS));
        }

        [Fact]
        public void Error_Last_Name_Max_Lentgh()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.LastName = GenerateStringOfLength('a', 101);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_MAX_LENGTH));
        }

        [Fact]
        public void Error_Last_Name_Invalid_Characters()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.LastName = GenerateStringOfLength('1', 10) + "123@#";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_INVALID_CHARACTERS));
        }

        // DATE OF BIRTH
        [Fact]
        public void Error_Date_Of_Birth_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.DateOfBirth = default;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_DATE_OF_BIRTH_EMPTY));
        }

        [Fact]
        public void Error_Date_Of_Birth_In_Future()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.DateOfBirth = DateTime.Now.AddDays(1);

            var result = validator.Validate(request);


            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_DATE_OF_BIRTH_IN_FUTURE));
        }

        // EMAIL
        //==================
        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_EMAIL_EMPTY));
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


        // AUXILIAR METHODS

        private string GenerateStringOfLength(char character, int length) => new string(character, length);        
    }
}
