using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;
using System.Text.Json;

namespace BTM.Products.Api.Services
{
    public  class TokenService : ITokenService
    {
        public async Task<TokenResult> RequestClientCredentialsTokenAsync()
        {
            var tokenEndpoint = "https://localhost:5001/connect/token";
            var clientId = "swagger";
            var clientSecret = "secret";
            var scope = "ProductsAPI.fullaccess";

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
            {
                Content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", scope)
            })
            };

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new TokenResult
                {
                    Success = false,
                    ErrorMessage = $"Token request failed: {response.StatusCode}"
                };
            }

            var json = JsonDocument.Parse(content);
            if (json.RootElement.TryGetProperty("access_token", out var token))
            {
                return new TokenResult
                {
                    Success = true,
                    AccessToken = token.GetString()
                };
            }

            return new TokenResult
            {
                Success = false,
                ErrorMessage = "Access token not found in response."
            };
        }
    }

}
