using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserUseCase _userUseCase;

        public UserService(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        public async Task<Domain.Entity.User> RegisterUserAsync(RequestRegisterUserJson request)
        {
            //var user = await _userUseCase.ExecuteRegisterUserAsync(request);

            //return user;

            return null;
        }
    }
}
