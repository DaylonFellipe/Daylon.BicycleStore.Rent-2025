using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserRepositoryBuilder
    {
        private readonly Mock<IUserRepository> _repository;

        public UserRepositoryBuilder() =>
            _repository = new Mock<IUserRepository>();

        public void ExistsUserWithEmailAsync(string email)
        {
            _repository.Setup(repository => repository.ExistsUserWithEmailAsync(email)).ReturnsAsync(true);
        }

        public IUserRepository Build() => _repository.Object;
      
    }
}
