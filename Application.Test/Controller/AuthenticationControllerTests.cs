using API.Controllers.Authentication.Microsoft.IdnetityWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Moq;
using Xunit;

namespace Application.Test.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<ITokenAcquisition> _mockTokenAcquisition;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockTokenAcquisition = new Mock<ITokenAcquisition>();
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new AuthenticationController(_mockTokenAcquisition.Object, _mockConfiguration.Object);
        }

        [Fact]
        public void Login_ShouldReturnOkResult_WithAuthUrl()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "AzureAd:Instance")]).Returns("instance/");
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "AzureAd:TenantId")]).Returns("tenantId");
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "AzureAd:ClientId")]).Returns("clientId");
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "AzureAd:Scopes")]).Returns("scopes");

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("callbackUrl");

            var controller = new AuthenticationController(null, mockConfiguration.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            // Act
            var result = controller.Login();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = new RouteValueDictionary(okResult.Value);
            Assert.True(response.ContainsKey("authUrl"));
            var authUrl = response["authUrl"].ToString();

            // Verify that authUrl is correctly formed
            Assert.Equal("instance/tenantId/oauth2/v2.0/authorize?client_id=clientId&response_type=code&redirect_uri=callbackUrl&response_mode=query&scope=offline_access%20scopes", authUrl);
        }
    }
}
