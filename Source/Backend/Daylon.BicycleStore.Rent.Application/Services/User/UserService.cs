using Daylon.BicycleStore.Rent.Application.DTOs.User;
using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace Daylon.BicycleStore.Rent.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserUseCase _userUseCase;

        public UserService(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
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
