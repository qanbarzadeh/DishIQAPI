using Application.DTO.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Application.Repository.Authentication;
using Application.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.ServiceTest
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IAuthUserRepository> _authUserRepositoryMock;
        private readonly Mock<IExternalLoginRepository> _externalLoginRepositoryMock;
        private readonly Mock<IUserEventRepository> _userEventRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IEntityCreationService> _entityCreationServiceMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _authUserRepositoryMock = new Mock<IAuthUserRepository>();
            _externalLoginRepositoryMock = new Mock<IExternalLoginRepository>();
            _userEventRepositoryMock = new Mock<IUserEventRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _userServiceMock = new Mock<IUserService>();
            _entityCreationServiceMock = new Mock<IEntityCreationService>();

            _authenticationService = new AuthenticationService(
                _configurationMock.Object,
                _userManagerMock.Object,
                _httpClientFactoryMock.Object,
                _authUserRepositoryMock.Object,
                _externalLoginRepositoryMock.Object,
                _userEventRepositoryMock.Object,
                _tokenServiceMock.Object,
                _userServiceMock.Object,
                _entityCreationServiceMock.Object);
        }

        [Fact]
        public async Task InitiateExternalAuthenticationAsync_ReturnsExpectedAuthorizationUrl()
        {
            // Arrange
            // Mocking Configuration Keys
            _configurationMock
                .Setup(x => x["AzureAd-ClientId"])
                .Returns("test_client_id");
            _configurationMock
                .Setup(x => x["DishIQ_Scope"])
                .Returns("test_scope");
            _configurationMock
                .Setup(x => x["AzureAd-RedirectUri"])
                .Returns("test_redirect_uri");



            // Act
            var result = await _authenticationService.InitiateExternalAuthenticationAsync("Microsoft");

            // Assert
            Assert.NotNull(result);
            Assert.Contains("test_client_id", result);
            Assert.Contains("test_scope", result);
        }
        [Fact]
        public async Task HandleExternalAuthenticationCallbackAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var tokenResponseData = new TokenResponse { AccessToken = "test_access_token", RefreshToken = "test_refresh_token", ExpiresIn = 3600 };
            var userInfoData = new UserInfoResponse { Email = "test@test.com", Id = "test_user_id" };
            var identityUser = new IdentityUser { Id = "test_id", Email = "test@test.com" };

            _tokenServiceMock.Setup(x => x.GetTokenResponseData(It.IsAny<string>())).ReturnsAsync(tokenResponseData);
            _userServiceMock.Setup(x => x.GetUserInfoData(It.IsAny<string>())).ReturnsAsync(userInfoData);
            _userServiceMock.Setup(x => x.GetIdentityUser(It.IsAny<UserInfoResponse>())).ReturnsAsync(identityUser);
            _entityCreationServiceMock.Setup(x => x.HandleUserEntitiesCreation(It.IsAny<string>(), It.IsAny<UserInfoResponse>(), It.IsAny<IdentityUser>())).Returns(Task.CompletedTask);

            // Act
            var result = await _authenticationService.HandleExternalAuthenticationCallbackAsync("Microsoft", "test_authorization_code");

            // Assert
            Assert.True(result.IsAuthenticated);
            Assert.Equal(tokenResponseData.AccessToken, result.Token);
            Assert.Equal(tokenResponseData.RefreshToken, result.RefreshToken);
            Assert.Equal(identityUser.Id, result.UserId);
        }


    }
}
