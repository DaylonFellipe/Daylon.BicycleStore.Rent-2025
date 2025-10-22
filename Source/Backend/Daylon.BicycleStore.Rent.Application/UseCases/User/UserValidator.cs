using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Exceptions;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.User
{
    internal static class UserValidator { }

    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
                .MaximumLength(100).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(ResourceMessagesException.NAME_INVALID_CHARACTERS);

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage(ResourceMessagesException.LAST_NAME_EMPTY)
                .MaximumLength(100).WithMessage(ResourceMessagesException.LAST_NAME_MAX_LENGTH)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(ResourceMessagesException.LAST_NAME_INVALID_CHARACTERS);

            RuleFor(user => user.DateOfBirth)
                .NotEmpty().WithMessage(ResourceMessagesException.DATE_OF_BIRTH_EMPTY)
                .Must(date => date <= DateTime.Now).WithMessage(ResourceMessagesException.DATE_OF_BIRTH_IN_FUTURE);

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID_FORMAT)
                .MaximumLength(256).WithMessage(ResourceMessagesException.EMAIL_MAX_LENGTH);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH)
                .MaximumLength(100).WithMessage(ResourceMessagesException.PASSWORD_MAX_LENGTH)
                .Matches(@"[A-Z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_UPPERCASE)
                .Matches(@"[a-z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_LOWERCASE)
                .Matches(@"\d").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_NUMBER)
                .Matches(@"[\W_]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_SPECIAL_CHAR);
        }
    }

    public class UpdateUserNameValidator : AbstractValidator<RequestUpdateUserNameJson>
    {
        public UpdateUserNameValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage(ResourceMessagesException.USER_ID_EMPTY).Must(id => id != Guid.Empty).WithMessage(ResourceMessagesException.USER_ID_INVALID);

            RuleFor(user => user.FirstName)
                .MaximumLength(100).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(ResourceMessagesException.NAME_INVALID_CHARACTERS);

            RuleFor(user => user.LastName)
                .MaximumLength(100).WithMessage(ResourceMessagesException.LAST_NAME_MAX_LENGTH)
                .Matches(@"^[a-zA-Z\s]+$").WithMessage(ResourceMessagesException.LAST_NAME_INVALID_CHARACTERS);
        }
    }

    public class UpdateUserEmailValidator : AbstractValidator<RequestUpdateUserEmailJson>
    {
        public UpdateUserEmailValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage(ResourceMessagesException.USER_ID_EMPTY).Must(id => id != Guid.Empty).WithMessage(ResourceMessagesException.USER_ID_INVALID);

            RuleFor(user => user.NewEmail)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID_FORMAT)
                .MaximumLength(256).WithMessage(ResourceMessagesException.EMAIL_MAX_LENGTH);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH)
                .MaximumLength(100).WithMessage(ResourceMessagesException.PASSWORD_MAX_LENGTH)
                .Matches(@"[A-Z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_UPPERCASE)
                .Matches(@"[a-z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_LOWERCASE)
                .Matches(@"\d").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_NUMBER)
                .Matches(@"[\W_]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_SPECIAL_CHAR);
        }
    }

    public class UpdateUserPasswordValidator : AbstractValidator<RequestUpdateUserPasswordJson>
    {
        public UpdateUserPasswordValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage(ResourceMessagesException.USER_ID_EMPTY).Must(id => id != Guid.Empty).WithMessage(ResourceMessagesException.USER_ID_INVALID);

            RuleFor(user => user.OldPassword)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH)
                .MaximumLength(100).WithMessage(ResourceMessagesException.PASSWORD_MAX_LENGTH)
                .Matches(@"[A-Z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_UPPERCASE)
                .Matches(@"[a-z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_LOWERCASE)
                .Matches(@"\d").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_NUMBER)
                .Matches(@"[\W_]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_SPECIAL_CHAR);

            RuleFor(user => user.NewPassword)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MIN_LENGTH)
                .MaximumLength(100).WithMessage(ResourceMessagesException.PASSWORD_MAX_LENGTH)
                .Matches(@"[A-Z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_UPPERCASE)
                .Matches(@"[a-z]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_LOWERCASE)
                .Matches(@"\d").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_NUMBER)
                .Matches(@"[\W_]").WithMessage(ResourceMessagesException.PASSWORD_REQUIRE_SPECIAL_CHAR)
                .NotEqual(user => user.OldPassword).WithMessage(ResourceMessagesException.PASSWORD_CANNOT_BE_SAME_AS_OLD);
        }
    }

    public class UpdateUserDateOfBirthValidator : AbstractValidator<RequestUpdateUserDateOfBirthJson>
    {
        public UpdateUserDateOfBirthValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage(ResourceMessagesException.USER_ID_EMPTY).Must(id => id != Guid.Empty).WithMessage(ResourceMessagesException.USER_ID_INVALID);

            RuleFor(user => user.NewDateOfBirth)
                 .NotEmpty().WithMessage(ResourceMessagesException.DATE_OF_BIRTH_EMPTY)
                 .Must(date => date <= DateTime.Now).WithMessage(ResourceMessagesException.DATE_OF_BIRTH_IN_FUTURE)
                 .NotEqual(user => user.OldDateOfBirth).WithMessage(ResourceMessagesException.DATE_OF_BIRTH_CANNOT_BE_SAME_AS_OLD);
        }
    }
}