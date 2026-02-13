using CommonTestUtilities.Requests.User;
using Daylon.BicycleStore.Rent.Application.DTOs.User;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.User.Update.Patch
{
    public class PatchUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PatchUserTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success_Update_User_Name()
        {
            var (userId, _) = await CreateAndGetUserIdAndPasswordAsync();

            var request = RequestUpdateUserNameJsonBuilder.Build(userId);

            var response = await _httpClient.PatchAsJsonAsync(
                $"api/User/name?id={request.Id}" +
                $"&firstName={request.FirstName}" +
                $"&LastName={request.LastName}",
                request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();

            updatedUser.Should().NotBeNull();
            updatedUser.Id.Should().Be(userId);
            updatedUser.Name.Should().Contain(request.FirstName);
            updatedUser.Name.Should().Contain(request.LastName);
        }

         [Fact]
        public async Task Success_Update_User_Email()
        {
            var (userId, userPassword) = await CreateAndGetUserIdAndPasswordAsync();

            var request = RequestUpdateUserEmailJsonBuilder.Build(userId);

            var response = await _httpClient.PatchAsJsonAsync(
                $"api/User/email?id={request.Id}" +
                $"&newEmail={request.NewEmail}" +
                $"&password={userPassword}",
                request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedUser = await response.Content.ReadFromJsonAsync<UserDto>();

            updatedUser.Should().NotBeNull();
            updatedUser.Id.Should().Be(userId);
            updatedUser.Email.Should().Be(request.NewEmail);
        }

        // AUXILIAR METHODS

        private async Task<(Guid Id, string? Password)> CreateAndGetUserIdAndPasswordAsync(int? count = 1)
        {
            var password = string.Empty;

            for (int i = 0; i < count; i++)
            {
                var request = RequestRegisterUserJsonBuilder.Build();

                password = request.Password;

                await _httpClient.PostAsJsonAsync("api/User", request);
            }

            var response = await _httpClient.GetAsync("api/User");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await ReadJsonAsync(response);
            return (json[0].GetProperty("id").GetGuid(), password);
        }
        private static async Task<JsonElement> ReadJsonAsync(HttpResponseMessage response)
        {
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var document = await JsonDocument.ParseAsync(responseBody);
            return document.RootElement.Clone();
        }
    }
}
