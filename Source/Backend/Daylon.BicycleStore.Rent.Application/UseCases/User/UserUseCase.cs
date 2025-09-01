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
            

            ValidateRequest(request, new RegisterUserValidator());

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

        private void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            if (request is not RequestRegisterUserJson registerUserRequest)
            {
                throw new ArgumentException("Invalid request type.", nameof(request));
            }

            var result = validator.Validate(request);

            if( ExistsEmail(registerUserRequest.Email))
                throw new ValidationException($"Validation failed: Email '{registerUserRequest.Email}' is already in use.");

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException($"Validation failed: {string.Join(", ", errors)}");
            }
        }

        private bool ExistsEmail(string email)
        {
            try
            {
                var user = _userRepository.GetUserByEmailAsync(email).Result;
                return user != null;
            }
            catch (AggregateException ex) when (ex.InnerException is KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
