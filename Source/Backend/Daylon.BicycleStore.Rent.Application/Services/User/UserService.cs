using Daylon.BicycleStore.Rent.Application.DTOs.User;
using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;

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
    }
}
