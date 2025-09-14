using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace UseCases.Test.User
{
    public class UserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.ExecuteRegisterUserAsync(request);

            result.Should().NotBeNull();
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Email.Should().Be(request.Email);
            result.DateOfBirth.Should().Be(request.DateOfBirth);
            result.Password.Should().NotBe(request.Password);
            result.Age.Should().Be(DateTime.Now.Year - request.DateOfBirth.Year);
            result.Active.Should().BeTrue();
            result.CreatedOn.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
        }

        private UserUseCase CreateUseCase()
        {
            var configuration = new ConfigurationBuilder().Build();
            var passwordEncripter = PBKDF2EncripterBuilder.Build(configuration);
            var userRepository = UserRepositoryBuilder.Build();

            return new UserUseCase(userRepository, passwordEncripter);
        }
    }
}
