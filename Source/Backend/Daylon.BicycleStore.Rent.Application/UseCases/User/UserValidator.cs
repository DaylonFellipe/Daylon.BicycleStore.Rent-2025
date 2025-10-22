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
                .NotEmpty().WithMessage("User Id is required.").Must(id => id != Guid.Empty).WithMessage("User Id must be a valid GUID.");

            RuleFor(user => user.OldPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.")
                .NotEqual(user => user.OldPassword).WithMessage("New password cannot be the same as the old password.");
        }
    }

    public class UpdateUserDateOfBirthValidator : AbstractValidator<RequestUpdateUserDateOfBirthJson>
    {
        public UpdateUserDateOfBirthValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage("User Id is required.").Must(id => id != Guid.Empty).WithMessage("User Id must be a valid GUID.");

            RuleFor(user => user.NewDateOfBirth)
                 .NotEmpty().WithMessage("Date of birth is required.")
                 .Must(date => date <= DateTime.Now).WithMessage("Date of birth must be in the past.")
                 .NotEqual(user => user.OldDateOfBirth).WithMessage("The new date of birth must be different from the current one.");
        }
    }
}