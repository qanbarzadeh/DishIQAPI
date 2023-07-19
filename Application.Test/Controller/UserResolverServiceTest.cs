using Application.Services.Authentication.Manual;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Controller
{
    public class UserResolverServiceTest
    {
        private readonly Mock<IUserManagerWrapper> _mockUserManager;
        private readonly UserResolverService _userResolverService;
        private readonly DefaultHttpContext _httpContext;

        public UserResolverServiceTest()
        {
            _mockUserManager = new Mock<IUserManagerWrapper>();
            _httpContext = new DefaultHttpContext();

            // Assuming UserResolverService takes IHttpContextAccessor and IUserManagerWrapper in its constructor
            _userResolverService = new UserResolverService(new HttpContextAccessor { HttpContext = _httpContext }, _mockUserManager.Object);
        }

        [Fact]
        public async Task GetUserFromToken_ShouldReturnUser_WhenTokenIsValid()
        {
            // Arrange
            string username = "valid_username";
            var user = new ApplicationUser { UserName = username };

            _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }));

            _mockUserManager.Setup(x => x.FindByNameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _userResolverService.GetUserFromToken();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.UserName);
        }
    }


}
