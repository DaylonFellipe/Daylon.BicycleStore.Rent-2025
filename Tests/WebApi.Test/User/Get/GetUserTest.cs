using CommonTestUtilities.Requests.User;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

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

            var responseData = await ReadJsonAsync(response);

            responseData.GetArrayLength().Should().BeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task Success_Get_Users_Active()
        {
            await CreateUserAsync(2);

            var response = await _httpClient.GetAsync("api/User?filterEnum=Active");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseData = await ReadJsonAsync(response);

            responseData.GetArrayLength().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Success_Get_Users_Inactive()
        {
            await CreateUserAsync(2);

            var response = await _httpClient.GetAsync("api/User?filterEnum=Inactive");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseData = await ReadJsonAsync(response);

            responseData.GetArrayLength().Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task Success_Get_User_By_Id()
        {
            var userId = await CreateAndGetUserIdAsync();

            var response = await GetUserByIdAsync(userId);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = await ReadJsonAsync(response);

            user.GetProperty("id").GetGuid().Should().Be(userId);
        }

        [Fact]
        public async Task Success_Get_User_By_Email()
        {
            await CreateUserAsync(1);

            var responseAllUsers = await _httpClient.GetAsync("api/User");

            responseAllUsers.StatusCode.Should().Be(HttpStatusCode.OK);

            var allUsers = await ReadJsonAsync(responseAllUsers);

            var firstUser = allUsers[0];

            var userEmail = firstUser.GetProperty("email").GetString();

            var response = await _httpClient.GetAsync($"api/User/nameOrEmail?nameOrEmail={userEmail}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = await ReadJsonAsync(response);

            users.GetArrayLength().Should().BeGreaterThanOrEqualTo(1);
            users[0].GetProperty("email").GetString().Should().Be(userEmail);
        }

        // AUXILIAR METHODS

        private async Task CreateUserAsync(int? count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var request = RequestRegisterUserJsonBuilder.Build();
                await _httpClient.PostAsJsonAsync("api/User", request);
            }
        }

        private async Task<Guid> CreateAndGetUserIdAsync()
        {
            await CreateUserAsync(1);

            var response = await _httpClient.GetAsync("api/User");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await ReadJsonAsync(response);
            return json[0].GetProperty("id").GetGuid();
        }

        private Task<HttpResponseMessage> GetUserByIdAsync(Guid userId)
        {
            return _httpClient.GetAsync($"api/User/{userId}");
        }

        private static async Task<JsonElement> ReadJsonAsync(HttpResponseMessage response)
        {
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var document = await JsonDocument.ParseAsync(responseBody);
            return document.RootElement.Clone();
        }
    }
}