using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.UseCases.User;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using static CommonTestUtilities.Repositories.Enum.RepositorySelectionEnum;

namespace WebApi.Test.User.Get
{
    public class GetUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public GetUserTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success_Get_Users()
        {
            // Creat a list of users
            await CreateUserAsync(3);

            var response = await _httpClient.GetAsync("api/User?filterEnum=All");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetArrayLength().Should().BeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task Success_Get_Users_Active()
        {
            await CreateUserAsync(2);

            var response = await _httpClient.GetAsync("api/User?filterEnum=Active");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetArrayLength().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Success_Get_Users_Inactive()
        {
            await CreateUserAsync(2);

            var response = await _httpClient.GetAsync("api/User?filterEnum=Inactive");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetArrayLength().Should().BeGreaterThanOrEqualTo(0);
        }


        // AUXILIAR METHODS

        private async Task CreateUserAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var request = RequestRegisterUserJsonBuilder.Build();
                await _httpClient.PostAsJsonAsync("api/User", request);
            }
        }
    }
}
