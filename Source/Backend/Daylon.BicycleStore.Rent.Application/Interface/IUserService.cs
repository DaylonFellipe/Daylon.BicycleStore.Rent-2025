using Daylon.BicycleStore.Rent.Application.DTOs.User;
using Daylon.BicycleStore.Rent.Communication.Request.User;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IUserService
    {
        // GET
        public Task<IList<Domain.Entity.User>> GetUsersAsync(UserStatusFilterEnum filterEnum);
        public Task<Domain.Entity.User> GetUserByIdAsync(Guid id);
        public Task<IList<Domain.Entity.User>> GetUserByNameOrEmailAsync(string nameOrEmail);

        // POST
        public Task<UserDto> RegisterUserAsync(RequestRegisterUserJson request);

    }
}
