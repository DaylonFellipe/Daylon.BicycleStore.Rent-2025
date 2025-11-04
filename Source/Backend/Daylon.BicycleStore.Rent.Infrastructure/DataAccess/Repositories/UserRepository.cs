using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


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

            return await query.ToListAsync() ?? throw new BicycleStoreException(ResourceMessagesException.USER_NO_FOUND);
        }

        public async Task<Domain.Entity.User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
                   ?? throw new BicycleStoreException(string.Format(ResourceMessagesException.USER_ID_NO_FOUND, id));
        }

        public async Task<Domain.Entity.User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new BicycleStoreException(string.Format(ResourceMessagesException.USER_EMAIL_NO_FOUND, email));
        }

        public async Task<IList<Domain.Entity.User>> GetUserByNameAsync(string searchName)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                throw new BicycleStoreException(ResourceMessagesException.USER_NAME_CANNOT_BE_NULL_OR_EMPTY);

            searchName = $"%{searchName.Trim()}%";

            var users = await _dbContext.Users
             .Where(u =>
                 EF.Functions.Like(u.FirstName + " " + u.LastName, searchName) ||
                 EF.Functions.Like(u.FirstName, searchName) ||
                 EF.Functions.Like(u.LastName, searchName))
             .ToListAsync();

            if (users == null || users.Count == 0)
                throw new BicycleStoreException(ResourceMessagesException.USER_NAME_NO_FOUND);

            return users;
        }

        public async Task<bool> ExistsUserWithEmailAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);

            if (user != null)
                return true;
            else
                return false;
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
