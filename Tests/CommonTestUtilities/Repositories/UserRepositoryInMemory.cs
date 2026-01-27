using Daylon.BicycleStore.Rent.Domain.Entity;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;

namespace CommonTestUtilities.Repositories
{
    public class UserRepositoryInMemory : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task AddUserAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(Guid id)
        {
            _users.RemoveAll(u => u.Id == id);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsUserWithEmailAsync(string email)
        {
            var exists = _users.Any(u => u.Email == email);
            return Task.FromResult(exists);
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email)
                ?? throw new KeyNotFoundException($"User with email '{email}' not found.");

            return Task.FromResult(user);
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id)
                ?? throw new KeyNotFoundException($"User with ID '{id}' not found.");

            return Task.FromResult(user);
        }

        public Task<IList<User>> GetUserByNameAsync(string searchName)
        {
            var user = _users
                .Where(u => $"{u.FirstName} {u.LastName}".Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                            u.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                            u.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult((IList<User>)user);
        }

        public Task<IList<User>> GetUsersAsync(UserStatusFilterEnum filterEnum = UserStatusFilterEnum.All)
        {
            var users = filterEnum switch
            {
                UserStatusFilterEnum.Active => _users.Where(u => u.Active).ToList(),
                UserStatusFilterEnum.Inactive => _users.Where(u => !u.Active).ToList(),
                UserStatusFilterEnum.All => _users.ToList(),
                _ => _users.ToList()
            };

            return Task.FromResult((IList<User>)users);
        }

        public Task SaveChangesAsync()
        {
            // In-memory implementation does not require saving changes.
            return Task.CompletedTask;
        }

        public Task UpdateUserAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
                _users.Add(user);
            }
            return Task.CompletedTask;
        }
    }
}
