﻿using Daylon.BicycleStore.Rent.Communication.Request.User;
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
                .NotEmpty().WithMessage("Name is required.")
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
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
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
}
