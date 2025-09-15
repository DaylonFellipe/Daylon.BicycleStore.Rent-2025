using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.User
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPBKDF2PasswordEncripter _passwordEncripter;

        public UserUseCase(
            IUserRepository userRepository,
            IPBKDF2PasswordEncripter passwordEncripter)
        {
            _userRepository = userRepository;
            _passwordEncripter = passwordEncripter;
        }

        // POST
        public async Task<Domain.Entity.User> ExecuteRegisterUserAsync(RequestRegisterUserJson request)
        {
            // Validate
            ValidateRegisterRequest(request, new RegisterUserValidator());

            //Check if Email is already registered
            if (await _userRepository.ExistsUserWithEmailAsync(request.Email))
                throw new ValidationException($"The email {request.Email} is already registered.");

            // Cryptographically Hash Password
            var hashedPassword = _passwordEncripter.HashPassword_PBKDF2Encripter(request.Password);

            // Map Properties and Created Entity
            var user = new Domain.Entity.User
            {
                // Person
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = DateTime.Now.Year - request.DateOfBirth.Year,
                DateOfBirth = request.DateOfBirth,

                // User
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = hashedPassword,
                CreatedOn = DateTime.Now,
                Active = true
            };

            // Save
            await _userRepository.AddUserAsync(user);

            return user;
        }

        // PATCH
        public async Task<Domain.Entity.User> ExecuteUpdateUserNameAsync(Guid id, string? firstName, string? lastName)
        {
            var requestUpdateName = new RequestUpdateUserNameJson
            {
                Id = id,
                FirstName = firstName!,
                LastName = lastName!
            };

            // Validate
            ValidateRequest(requestUpdateName, new UpdateUserNameValidator());

            // Map Properties
            var user = await _userRepository.GetUserByIdAsync(id);

            if (!string.IsNullOrWhiteSpace(firstName))
                user.FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName))
                user.LastName = lastName;

            // Save
            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        public async Task<Domain.Entity.User> ExecuteUpdateUserEmailAsync(Guid id, string newEmail, string password)
        {
            var requestUpdateEmail = new RequestUpdateUserEmailJson
            {
                Id = id,
                NewEmail = newEmail,
                Password = password
            };

            // Validate
            ValidateRequest(requestUpdateEmail, new UpdateUserEmailValidator());

            //Check if Email is already registered
            if (await _userRepository.ExistsUserWithEmailAsync(newEmail))
                throw new ValidationException($"The email {newEmail} is already registered.");

            // Verify Password
            var user = await _userRepository.GetUserByIdAsync(id);

            if (!_passwordEncripter.VerifyPassword_PBKDF2Encripter(password, user.Password))
                throw new ValidationException("The password is incorrect.");

            // Check if New Email is already registered
            if (await _userRepository.ExistsUserWithEmailAsync(newEmail))
                throw new ValidationException("The new email is already registered.");

            // Change to New Email
            user.Email = newEmail;

            // Save
            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        public async Task<Domain.Entity.User> ExecuteUpdateUserPasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            var requestUpdatePassword = new RequestUpdateUserPasswordJson
            {
                Id = id,
                OldPassword = oldPassword,
                NewPassword = newPassword
            };

            // Validate
            ValidateRequest(requestUpdatePassword, new UpdateUserPasswordValidator());

            // Verify Old Password
            var user = await _userRepository.GetUserByIdAsync(id);

            if (!_passwordEncripter.VerifyPassword_PBKDF2Encripter(oldPassword, user.Password))
                throw new ValidationException("The old password is incorrect.");

            // Change to New Password
            var hashedNewPassword = _passwordEncripter.HashPassword_PBKDF2Encripter(newPassword);

            user.Password = hashedNewPassword;

            // Save
            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        public async Task<Domain.Entity.User> ExecuteUpdateUserDateOfBirthAsync(Guid id, DateTime newDateOfBirth)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var requestUpdateDateOfBirth = new RequestUpdateUserDateOfBirthJson
            {
                Id = id,
                NewDateOfBirth = newDateOfBirth,
                OldDateOfBirth = user.DateOfBirth

            };

            // Validate
            ValidateRequest(requestUpdateDateOfBirth, new UpdateUserDateOfBirthValidator());

            // Map Properties
            user.DateOfBirth = newDateOfBirth;
            user.Age = DateTime.Now.Year - newDateOfBirth.Year;

            // Save
            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        // PUT
        public async Task<Domain.Entity.User> ExecuteUpdateUserStatusAsync(Domain.Entity.User user)
        {
            switch
                (user.Active)
            {
                case true:
                    user.Active = false;
                    break;

                case false:
                    user.Active = true;
                    break;
            }

            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        // EXTENSIONS METHODS
        private async void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException($"Validation failed: {string.Join(", ", errors)}");
            }
        }
        private async void ValidateRegisterRequest<T>(T request, AbstractValidator<T> validator)
        {
            if (request is not RequestRegisterUserJson registerUserRequest)
                throw new ValidationException("Invalid request type.");

            if (await _userRepository.ExistsUserWithEmailAsync(registerUserRequest.Email))
                throw new ValidationException("The new email is already registered.");

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException($"Validation failed: {string.Join(", ", errors)}");
            }
        }
    }
}
