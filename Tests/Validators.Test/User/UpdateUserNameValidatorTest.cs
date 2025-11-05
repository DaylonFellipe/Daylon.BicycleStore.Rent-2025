using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.User
{
    public class UpdateUserNameValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // ID

        [Fact]
        public void Error_Id_Empty()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_EMPTY));
        }

        [Fact]
        public void Error_Id_Invalid()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_INVALID));
        }

        // FIRST NAME

        [Fact]
        public void Error_Name_Max_Lentgh()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.FirstName = GenerateStringOfLength('a', 101);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_MAX_LENGTH));
        }

        [Fact]
        public void Error_Name_Invalid_Characters()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.FirstName = GenerateStringOfLength('1', 10) + "123@#";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_NAME_INVALID_CHARACTERS));
        }

        // LAST NAME

        [Fact]
        public void Error_Last_Name_Max_Lentgh()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.LastName = GenerateStringOfLength('a', 101);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_MAX_LENGTH));
        }

        [Fact]
        public void Error_Last_Name_Invalid_Characters()
        {
            var validator = new UpdateUserNameValidator();

            var request = RequestUpdateUserNameJsonBuilder.Build();
            request.LastName = GenerateStringOfLength('1', 10) + "123@#";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_LAST_NAME_INVALID_CHARACTERS));
        }

        // AUXILIAR METHODS

        public string GenerateStringOfLength(char character, int length) => new string(character, length);
    }
}

