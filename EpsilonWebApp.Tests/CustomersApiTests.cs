using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EpsilonWebApp.Tests
{
    public class CustomersApiTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;

        public CustomersApiTests(ApiWebApplicationFactory factory) => _factory = factory;

        [Fact]
        public async Task GetCustomers_WithoutToken_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/customers");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomers_WithValidToken_Returns200()
        {
            var client = _factory.CreateClient();
            var token = await GetTokenAsync(client, "admin", "password123");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/api/customers");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_Returns401()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync(
                "/api/auth/login", new { username = "admin", password = "wrong" });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        private static async Task<string> GetTokenAsync(HttpClient client, string username, string password)
        {
            var response = await client.PostAsJsonAsync(
                "/api/auth/login", new { username, password });
            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return payload!.Token;
        }

        private record TokenResponse(string Token);
    }
}
