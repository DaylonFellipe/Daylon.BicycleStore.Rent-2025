using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Domain.Repositories
{
    public interface IUserRepository
    {

        // DB
        Task SaveChangesAsync();

        // GET
        Task<IList<Domain.Entity.User>> GetUsersAsync(UserStatusFilterEnum filterEnum = UserStatusFilterEnum.All);
        Task<Domain.Entity.User> GetUserByIdAsync(Guid id);
        Task<Domain.Entity.User> GetUserByEmailAsync(string email);

        // POST
        Task AddUserAsync(Domain.Entity.User user);

        // PUT
        Task UpdateUserAsync(Domain.Entity.User user);

        // DELETE
        Task DeleteUserAsync(Guid id);
    }
}
