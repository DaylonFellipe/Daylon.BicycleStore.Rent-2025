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
using static CommonTestUtilities.Repositories.Enum.RepositorySelectionEnum;

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

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(email: request.Email);

            Func<Task> action = async () => await useCase.ExecuteRegisterUserAsync(request);

            await action.Should().ThrowAsync<BicycleStoreException>()
                .WithMessage(ResourceMessagesException.USER_EMAIL_ALREADY_REGISTERED);
        }

        // PATCH

        [Fact]
        public async Task Success_Update_Name()
        {
            // Use InMemory Repository to persist user between calls
            var useCase = CreateUseCase(RepositoryEnum.InMemoryRepository);

            // Create a user
            var requestUser = RequestRegisterUserJsonBuilder.Build();

            var userResult = await useCase.ExecuteRegisterUserAsync(requestUser);

            // Update user
            var request = RequestUpdateUserNameJsonBuilder.Build();

            var result = await useCase.ExecuteUpdateUserNameAsync(userResult.Id, request.FirstName, request.LastName);

            result.Should().NotBeNull();
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
        }

        [Fact]
        public async Task Success_Update_Email()
        {
            var useCase = CreateUseCase(RepositoryEnum.InMemoryRepository);

            var requestUser = RequestRegisterUserJsonBuilder.Build();

            // Unencrypted password to update email
            var unencryptedPassword = requestUser.Password;

            var userResult = await useCase.ExecuteRegisterUserAsync(requestUser);

            // Update user
            var request = RequestUpdateUserEmailJsonBuilder.Build(userResult.Id, userResult.Password);

            var result = await useCase.ExecuteUpdateUserEmailAsync(request.Id, request.NewEmail, unencryptedPassword);

            result.Should().NotBeNull();
            result.Email.Should().Be(request.NewEmail);
        }

        [Fact]
        public async Task Success_Update_Password()
        {
            var useCase = CreateUseCase(RepositoryEnum.InMemoryRepository);

            var requestUser = RequestRegisterUserJsonBuilder.Build();
            var unencryptedOldPassword = requestUser.Password;

            var userResult = await useCase.ExecuteRegisterUserAsync(requestUser);
            var unchangedPassword = userResult.Password;    

            // Update user
            var request = RequestUpdateUserPasswordJsonBuilder.Build(userResult.Id, unencryptedOldPassword);

            var result = await useCase.ExecuteUpdateUserPasswordAsync(request.Id, request.OldPassword, request.NewPassword);

            result.Should().NotBeNull();
            result.Password.Should().NotBe(unchangedPassword);
        }

        //[Fact]
        //public async Task Error_xxxxxxx()
        //{
        //    var request = RequestRegisterUserJsonBuilder.Build();

        //    var useCase = CreateUseCase(request.Email);

        //    Func<Task> action = async () => await useCase.ExecuteRegisterUserAsync(request);

        //    await action.Should().ThrowAsync<BicycleStoreException>()
        //        .WithMessage(ResourceMessagesException.USER_EMAIL_ALREADY_REGISTERED);
        //}

        // AUXILIAR METHODS

        private UserUseCase CreateUseCase(
            RepositoryEnum repository = RepositoryEnum.MockRepository,
            string? email = null)
        {
            _ = new ConfigurationBuilder().Build();
            var passwordEncripter = PBKDF2EncripterBuilder.Build();

            // Repository - Mock = 0 | InMemory = 1
            switch (repository)
            {
                case RepositoryEnum.MockRepository:
                    {
                        var userRepositoryBuilder = new UserRepositoryBuilder();

                        if (!string.IsNullOrEmpty(email))
                            userRepositoryBuilder.ExistsUserWithEmailAsync(email);

                        return new UserUseCase(userRepositoryBuilder.Build(), passwordEncripter);
                    }

                case RepositoryEnum.InMemoryRepository:
                    {
                        var userRepositoryBuilder = new UserRepositoryInMemory();

                        if (!string.IsNullOrEmpty(email))
                            userRepositoryBuilder.ExistsUserWithEmailAsync(email);

                        return new UserUseCase(userRepositoryBuilder, passwordEncripter);
                    }

                default:
                    throw new ArgumentException("Invalid repository selection.");
            }
        }
    }
}