﻿using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Configuration;

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

            await action.Should().ThrowAsync<ValidationException>()
                       .WithMessage($"The email {request.Email} is already registered.");
        }

        private UserUseCase CreateUseCase(string? email = null)
        {
            var configuration = new ConfigurationBuilder().Build();
            var passwordEncripter = PBKDF2EncripterBuilder.Build(configuration);
            var userRepositoryBuilder = new UserRepositoryBuilder();

            if (!string.IsNullOrEmpty(email))
                userRepositoryBuilder.ExistsUserWithEmailAsync(email);

            return new UserUseCase(userRepositoryBuilder.Build(), passwordEncripter);
        }
    }
}

