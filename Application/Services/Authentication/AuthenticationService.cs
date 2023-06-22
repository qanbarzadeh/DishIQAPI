using Application.DTO.Authentication;
using Application.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using Domain.Entities.UserRegistration;
using Domain.Enums.UserRegistration;
using System.Web;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Domain.Entities.Factories.UserRegistration;

namespace Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpClient _httpClient;

        public AuthenticationService(IConfiguration configuration, UserManager<IdentityUser> userManager, HttpClient httpClient)
        {
            _configuration = configuration;
            _userManager = userManager;
            _httpClient = httpClient;
        }

        public async Task<string> InitiateExternalAuthenticationAsync(string provider, string redirectUri)
        {
            // Validate the provider
            if (provider != "Microsoft")
            {
                throw new ArgumentException("Unsupported provider");
            }

            // Define the query parameters for the authorization request
            var queryParams = new Dictionary<string, string>()
            {
            { "client_id", _configuration["AzureAd-ClientId"] },
            { "response_type", "code" },
            { "redirect_uri", redirectUri },
            { "response_mode", "query" },
            { "scope", _configuration["DishIQ_Scope"] },
            { "state", Guid.NewGuid().ToString() } // Use a random value for the state parameter to mitigate CSRF attacks
            };

            // Construct the authorization request URL
            var url = new UriBuilder("https://login.microsoftonline.com/common/oauth2/v2.0/authorize")
            {
                Query = ToQueryString(queryParams)
            };

            return url.ToString();
        }

        private static string ToQueryString(Dictionary<string, string> queryParams)
        {
            var array = queryParams.Select(kvp => string.Format("{0}={1}", HttpUtility.UrlEncode(kvp.Key), HttpUtility.UrlEncode(kvp.Value)))
                                   .ToArray();
            return string.Join("&", array);
        }

        public async Task<AuthenticationResult> HandleExternalAuthenticationCallbackAsync(string provider, string authorizationCode)
        {
            if (provider != "Microsoft")
            {
                throw new ArgumentException("Unsupported provider");
            }

            // Load the Azure AD client secret from Azure Key Vault
            var keyVaultUri = _configuration["AzureAd-ClientSecret"];
            var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync("AzureAd-ClientSecret");

            // Exchange the authorization code for an access token
            var tokenResponse = await _httpClient.PostAsync("https://login.microsoftonline.com/common/oauth2/v2.0/token", new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _configuration["AzureAd-ClientId"]),
            new KeyValuePair<string, string>("scope", _configuration["DishIQ_Scope"]),
            new KeyValuePair<string, string>("code", authorizationCode),
            new KeyValuePair<string, string>("redirect_uri", _configuration["AzureAd-RedirectUri"]),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("client_secret", secret.Value),
              }));

            tokenResponse.EnsureSuccessStatusCode();

            var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseData = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseContent);

            // Call the Microsoft Graph API to get the user's email address and other information
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponseData.AccessToken);
            var userInfoResponse = await _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me");
            userInfoResponse.EnsureSuccessStatusCode();

            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfoData = JsonConvert.DeserializeObject<UserInfoResponse>(userInfoContent);

            // Finding the IdentityUser object from the email obtained
            var identityUser = await _userManager.FindByEmailAsync(userInfoData.Email);

            if (identityUser == null)
            {
                identityUser = new IdentityUser { UserName = userInfoData.Email, Email = userInfoData.Email };
                var result = await _userManager.CreateAsync(identityUser);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create user"); // Replace with your own error handling
                }
            }

            // Creating an AuthUser object
            var authUser = UserFactory.CreateUser(identityUser.Email, identityUser.UserName);
            // Creating the ExternalLogin object

            var externalLogin = ExternalLoginFactory.CreateExternalLogin(provider, userInfoData.Id, authUser);
            
            // Create UserEvent object
            var userEvent = UserEventFactory.CreateUserEvent(authUser, EventType.Login);


            // TODO: Save the AuthUser, ExternalLogin, and UserEvent objects to the database. 
            // This will depend on how you've setup your DbContext or repositories.

            return new AuthenticationResult
            {
                IsAuthenticated = true,
                Token = tokenResponseData.AccessToken,
                RefreshToken = tokenResponseData.RefreshToken,
                ExpiryDate = DateTime.UtcNow.AddSeconds(tokenResponseData.ExpiresIn),
                UserId = identityUser.Id,
                Errors = null // No errors
            };
        }

    }
}
