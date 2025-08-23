using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.User
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST
        public async Task<Domain.Entity.User> ExecuteRegisterUserAsync(RequestRegisterUserJson request)
        {
            // Validate
            ValidateRequest(request, new RegisterUserValidator());

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
                Password = request.Password,
                CreatedOn = DateTime.Now,
                Active = true
            };

            // Save
            await _userRepository.AddUserAsync(user);

            return user;
        }

        private void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException($"Validation failed: {string.Join(", ", errors)}");
            }
        }
    }
}
