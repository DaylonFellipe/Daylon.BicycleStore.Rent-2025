using CommonTestUtilities.Requests.User;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.User.Update.Put
{
    public class PutUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PutUserTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success_Update_User_Status()
        {
            var userId = await CreateAndGetUserIdAsync();

            var content = JsonContent.Create(new { });
            var response = await _httpClient.PutAsync($"api/User/{userId}/changeStatus", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");

            var responseText = await response.Content.ReadAsStringAsync();

            responseText.Should().Contain("User Status Update to");
        }

        // AUXILIAR METHODS

        private async Task<Guid> CreateAndGetUserIdAsync(int? count = 1)
        {
            for (int i = 0; i < count; i++) ;
            {
                var request = RequestRegisterUserJsonBuilder.Build();
                await _httpClient.PostAsJsonAsync("api/User", request);
            }

            var response = await _httpClient.GetAsync("api/User");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await ReadJsonAsync(response);
            return json[0].GetProperty("id").GetGuid();
        }
        private static async Task<JsonElement> ReadJsonAsync(HttpResponseMessage response)
        {
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var document = await JsonDocument.ParseAsync(responseBody);
            return document.RootElement.Clone();
        }
    }
}
