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
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");

            RuleFor(user => user.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(date => date <= DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(user => user.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
        }
    }

    public class UpdateUserNameValidator : AbstractValidator<RequestUpdateUserNameJson>
    {
        public UpdateUserNameValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage("User Id is required.").Must(id => id != Guid.Empty).WithMessage("User Id must be a valid GUID.");

            RuleFor(user => user.FirstName)
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");

            RuleFor(user => user.LastName)
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");
        }
    }

    public class UpdateUserEmailValidator : AbstractValidator<RequestUpdateUserEmailJson>
    {
        public UpdateUserEmailValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Id)
                .NotEmpty().WithMessage("User Id is required.").Must(id => id != Guid.Empty).WithMessage("User Id must be a valid GUID.");

            RuleFor(user => user.NewEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
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