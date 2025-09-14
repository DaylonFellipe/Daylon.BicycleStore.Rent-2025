using Daylon.BicycleStore.Rent.Domain.Repositories;
using Daylon.BicycleStore.Rent.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserRepositoryBuilder
    {
        public static IUserRepository Build()
        {
            var mock = new Mock<IUserRepository>();

            return mock.Object;
        }
    }
}
