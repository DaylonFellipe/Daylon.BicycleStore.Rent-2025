using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentAssertions;

namespace Validators.Test.User
{
    public class UpdateUserDateOfBirthValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        // ID

        [Fact]
        public void Error_Id_Empty()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_EMPTY));
        }

        [Fact]
        public void Error_Id_Invalid()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();
            request.Id = Guid.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_ID_INVALID));
        }

        // DATE OF BIRTH

        [Fact]
        public void Error_Date_Of_Birth_Empty()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();
            request.NewDateOfBirth = default;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_DATE_OF_BIRTH_EMPTY));
        }

        [Fact]
        public void Error_Date_Of_Birth_In_Future()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();
            request.NewDateOfBirth = DateTime.Now.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_DATE_OF_BIRTH_IN_FUTURE));
        }

        [Fact]
        public void Error_Date_Of_Birth_Cannot_Be_Same_As_Old()
        {
            var validator = new UpdateUserDateOfBirthValidator();

            var request = RequestUpdateUserDateOfBirthJsonBuilder.Build();
            request.OldDateOfBirth = request.NewDateOfBirth;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage.Contains(ResourceMessagesException.USER_DATE_OF_BIRTH_CANNOT_BE_SAME_AS_OLD));
        }
    }
}