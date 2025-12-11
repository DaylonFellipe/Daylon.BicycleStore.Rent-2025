using Azure.Core;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using Daylon.BicycleStore.Rent.Domain.Security.Cryptography;
using Daylon.BicycleStore.Rent.Exceptions;
using Daylon.BicycleStore.Rent.Exceptions.ExceptionBase;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;

// The request properties are tested in Validator.Test - empty fields, invalid email, weak password, future date of birth, etc.

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

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            Func<Task> action = async () => await useCase.ExecuteRegisterUserAsync(request);

            await action.Should().ThrowAsync<BicycleStoreException>()
                .WithMessage(ResourceMessagesException.USER_EMAIL_ALREADY_REGISTERED);
        }

        [Fact]
        public async Task Success_Verify_Password_Encryter()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            // Mock
            var passwordEncripterMock = new Mock<IPBKDF2PasswordEncripter>();
            passwordEncripterMock.Setup(p => p.HashPassword_PBKDF2Encripter(It.IsAny<string>()))
                .Returns("hashed_password");

            var userRepositoryBuilder = new UserRepositoryBuilder();

            var userRepository = userRepositoryBuilder.Build();

            var useCase = new UserUseCase(userRepository, passwordEncripterMock.Object);

            await useCase.ExecuteRegisterUserAsync(request);

            passwordEncripterMock.Verify(
                p => p.HashPassword_PBKDF2Encripter(request.Password),
                Times.Once);
        }

        // AUXILIAR METHODS
        private UserUseCase CreateUseCase(string? email = null)
        {
            var configuration = new ConfigurationBuilder().Build();
            var passwordEncripter = PBKDF2EncripterBuilder.Build();
            var userRepositoryBuilder = new UserRepositoryBuilder();

            if (!string.IsNullOrEmpty(email))
                userRepositoryBuilder.ExistsUserWithEmailAsync(email);

            return new UserUseCase(userRepositoryBuilder.Build(), passwordEncripter);
        }
    }
}