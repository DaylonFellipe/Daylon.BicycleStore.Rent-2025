using Daylon.BicycleStore.Rent.Application.DTOs.User;
using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Daylon.BicycleStore.Rent.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserUseCase userUseCase,
            IUserRepository userRepository)
        {
            _userUseCase = userUseCase;
            _userRepository = userRepository;
        }

        // GET
        public async Task<IList<Domain.Entity.User>> GetUsersAsync(UserStatusFilterEnum filterEnum)
        {
            var users = await _userRepository.GetUsersAsync(filterEnum);

            return users;
        }

        public async Task<Domain.Entity.User> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return user;
        }

        public async Task<IList<Domain.Entity.User>> GetUserByNameOrEmailAsync(string nameOrEmail)
        {
           var verification = IsEmail(nameOrEmail);

            if (verification)
            {
                var user = await _userRepository.GetUserByEmailAsync(nameOrEmail);

                var users = new List<Domain.Entity.User> { user };

                return users;
            }

            else
            {
                var users = await _userRepository.GetUserByNameAsync(nameOrEmail);

                return users;
            }
        }

        // POST
        public async Task<UserDto> RegisterUserAsync(RequestRegisterUserJson request)
        {
            var userEntity = await _userUseCase.ExecuteRegisterUserAsync(request);

            return new UserDto
            {
                Id = userEntity.Id,
                Name = $"{userEntity.FirstName} {userEntity.LastName}",
                Email = userEntity.Email,
                Password = userEntity.Password
            };
        }

        // DELETE
        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);
        }



        // EXTENSION SUPORTS
        private static bool IsEmail(string input)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(input);
                return addr.Address == input;
            }
            catch
            {
                return false;
            }
        }
    }
}
