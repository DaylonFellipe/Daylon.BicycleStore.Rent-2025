using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.User
{
    public class UpdateUserEmailValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // ID

        [Fact]
        public void Error_Id_Empty()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_EMPTY));
        }

        [Fact]
        public void Error_Id_Invalid()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_INVALID));
        }

        // NEW EMAIL

        [Fact]
        public void Error_Email_Empty()
        {

            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.NewEmail = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_EMAIL_EMPTY));
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_EMAIL_INVALID_FORMAT));
        }

        [Fact]
        public void Error_Email_Invalid()
        {

            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.NewEmail = "email.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_EMAIL_INVALID_FORMAT));
        }

        [Fact]
        public void Error_Email_MaxLengh()
        {

            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.NewEmail = GenerateStringOfLength('a', 257) + "@gmail.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_EMAIL_MAX_LENGTH));
        }

        // PASSWORD

        [Fact]
        public void Error_New_Password_Empty()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_EMPTY));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void Error_New_Password_Min_Lenght(int passwordLength)
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MIN_LENGTH));
        }

        [Fact]
        public void Error_New_Password_Max_Lenght()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = GenerateStringOfLength('a', 256) + request.Password;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MAX_LENGTH));
        }

        [Fact]
        public void Error_New_Password_No_Upercase()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = "@123abdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_UPPERCASE));
        }

        [Fact]
        public void Error_New_Password_No_Lowercase()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = "@123ABCDEF";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_LOWERCASE));
        }

        [Fact]
        public void Error_New_Password_No_Number()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = "@Abdefgh";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_NUMBER));
        }

        [Fact]
        public void Error_New_Password_No_Special_Char()
        {
            var validator = new UpdateUserEmailValidator();

            var request = RequestUpdateUserEmailJsonBuilder.Build();
            request.Password = "123Abcdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_SPECIAL_CHAR));
        }

        // AUXILIAR METHODS

        public string GenerateStringOfLength(char character, int length) => new string(character, length);
    }
}
