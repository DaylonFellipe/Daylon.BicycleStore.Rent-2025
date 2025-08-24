using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BicycleStoreDbContext _dbContext;

        public UserRepository(BicycleStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // DB
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        // GET
        public async Task<IList<Domain.Entity.User>> GetUsersAsync(UserStatusFilterEnum filterEnum = UserStatusFilterEnum.All)
        {
            IQueryable<Domain.Entity.User> query = _dbContext.Users;

            switch (filterEnum)
            {
                case UserStatusFilterEnum.Active:
                    query = query.Where(query => query.Active.Equals(true));
                    break;


                case UserStatusFilterEnum.Inactive:
                    query = query.Where(query => query.Active.Equals(false));
                    break;

                case UserStatusFilterEnum.All:
                default:
                    break;
            }

            return await query.ToListAsync() ?? throw new Exception("No users found.");
        }

        public async Task<Domain.Entity.User> GetUserByIdAsync(Guid id)

        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
                   ?? throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        public async Task<Domain.Entity.User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email)
                   ?? throw new KeyNotFoundException($"User with email {email} not found.");
        }

        // POST
        public async Task AddUserAsync(Domain.Entity.User user)
        {
            await _dbContext.Users.AddAsync(user);
            await SaveChangesAsync();
        }

        // PUT
        public async Task UpdateUserAsync(Domain.Entity.User user)
        {
            _dbContext.Users.Update(user);
            await SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            _dbContext.Users.Remove(user);
            await SaveChangesAsync();
        }
    }
}
