using Daylon.BicycleStore.Rent.Application.DTOs.User;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IUserService
    {
        // POST
        public Task<UserDto> RegisterUserAsync(RequestRegisterUserJson request);

    }
}
