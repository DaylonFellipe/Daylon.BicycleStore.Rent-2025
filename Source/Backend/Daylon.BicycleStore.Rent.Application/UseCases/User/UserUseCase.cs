using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Domain.Repositories;

namespace Daylon.BicycleStore.Rent.Application.UseCases.User
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
