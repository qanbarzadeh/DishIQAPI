using Application.DTO.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services.Authentication.Helpers
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public TokenService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<TokenResponse> GetTokenResponseData(string authorizationCode)
        {
            var keyVaultUri = _configuration["AzureAd-ClientSecret"];
            var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync("AzureAd-ClientSecret");

            var tokenResponse = await _httpClient.PostAsync("https://login.microsoftonline.com/common/oauth2/v2.0/token", new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _configuration["AzureAd-ClientId"]),
            new KeyValuePair<string, string>("scope", _configuration["DishIQ_Scope"]),
            new KeyValuePair<string, string>("code", authorizationCode),
            new KeyValuePair<string, string>("redirect_uri", _configuration["AzureAd-RedirectUri"]),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("client_secret", secret.Value),
        }));

            if (!tokenResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to exchange authorization code for access token"); 
            }
            
            var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseData = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseContent);
            if (tokenResponseData == null)
            {
                throw new Exception("Failed to deserialize token response"); 
            }
            return tokenResponseData;
        }
    }
}
