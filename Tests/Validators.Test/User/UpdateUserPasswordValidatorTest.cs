using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.User
{
    public class UpdateUserPasswordValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // ID

        [Fact]
        public void Error_Id_Empty()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_EMPTY));
        }

        [Fact]
        public void Error_Id_Invalid()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_INVALID));
        }

        // NEW PASSWORD

        [Fact]
        public void Error_New_Password_Empty()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = string.Empty;

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
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MIN_LENGTH));
        }

        [Fact]
        public void Error_New_Password_Max_Lenght()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = GenerateStringOfLength('a', 256) + request.NewPassword;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MAX_LENGTH));
        }

        [Fact]
        public void Error_New_Password_No_Upercase()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = "@123abdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_UPPERCASE));
        }

        [Fact]
        public void Error_New_Password_No_Lowercase()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = "@123ABCDEF";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_LOWERCASE));
        }

        [Fact]
        public void Error_New_Password_No_Number()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = "@Abdefgh";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_NUMBER));
        }

        [Fact]
        public void Error_New_Password_No_Special_Char()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = "123Abcdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_SPECIAL_CHAR));
        }

        [Fact]
        public void Error_New_Password_Cannot_Be_Same_As_Old()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.NewPassword = request.OldPassword;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_CANNOT_BE_SAME_AS_OLD));
        }

        // OLD PASSWORD

        [Fact]
        public void Error_Old_Password_Empty()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = string.Empty;

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
        public void Error_Old_Password_Min_Lenght(int passwordLength)
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MIN_LENGTH));
        }

        [Fact]
        public void Error_Old_Password_Max_Lenght()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = GenerateStringOfLength('a', 256) + request.OldPassword;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_MAX_LENGTH));
        }

        [Fact]
        public void Error_Old_Password_No_Upercase()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = "@123abdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_UPPERCASE));
        }

        [Fact]
        public void Error_Old_Password_No_Lowercase()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = "@123ABCDEF";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_LOWERCASE));
        }

        [Fact]
        public void Error_Old_Password_No_Number()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = "@Abdefgh";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_NUMBER));
        }

        [Fact]
        public void Error_Old_Password_No_Special_Char()
        {
            var validator = new UpdateUserPasswordValidator();

            var request = RequestUpdateUserPasswordJsonBuilder.Build();
            request.OldPassword = "123Abcdef";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_PASSWORD_REQUIRE_SPECIAL_CHAR));
        }
        
        // AUXILIAR METHODS

        public string GenerateStringOfLength(char character, int length) => new string(character, length);
    }