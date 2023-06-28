using Application.DTO.Authentication;
using Application.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Application.Repository.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Domain.Exceptions.Authentication;

namespace Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IExternalLoginRepository _externalLoginRepository;
        private readonly IUserEventRepository _userEventRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IEntityCreationService _entityCreationService;

        public AuthenticationService(IConfiguration configuration,
                                      UserManager<IdentityUser> userManager,
                                      IHttpClientFactory httpClientFactory,
                                      IAuthUserRepository authUserRepository,
                                      IExternalLoginRepository externalLoginRepository,
                                      IUserEventRepository userEventRepository,
                                      ITokenService tokenService,
                                      IUserService userService,
                                      IEntityCreationService entityCreationService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _authUserRepository = authUserRepository;
            _externalLoginRepository = externalLoginRepository;
            _userEventRepository = userEventRepository;
            _tokenService = tokenService;
            _userService = userService;
            _entityCreationService = entityCreationService;
        }

        public async Task<string> InitiateExternalAuthenticationAsync(string provider)
        {
            // Validate the provider
            if (provider != "Microsoft")
            {
                throw new AuthenticationException("Unsupported provider");
            }


            string redirectUri = _configuration["AzureAd-RedirectUri"];

            // Define the query parameters for the authorization request
            var queryParams = new Dictionary<string, string>()
            {
            { "client_id", _configuration["AzureAd-ClientId"] },
            { "response_type", "code" },
            { "redirect_uri", redirectUri },
            { "response_mode", "query" },
             { "scope", $"openid profile email {_configuration["DishIQ_Scope"]}" },
            { "state", Guid.NewGuid().ToString() } // Use a random value for the state parameter to mitigate CSRF attacks
            };

            // Construct the authorization request URL
            var url = new UriBuilder("https://login.microsoftonline.com/common/oauth2/v2.0/authorize") //todo: store link in configuration
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
                throw new AuthenticationException($"{provider} is not supported.");
            }

            try
            {
                var tokenResponseData = await _tokenService.GetTokenResponseData(authorizationCode);

                var userInfoData = await _userService.GetUserInfoData(tokenResponseData.AccessToken);

                var identityUser = await _userService.GetIdentityUser(userInfoData);

                await _entityCreationService.HandleUserEntitiesCreation(provider, userInfoData, identityUser);

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
            catch (Exception e)
            {
                throw new TokenExchangeException("Error exchanging token", e);
            }
        }
    }
}
