using Application.Interfaces.Authentication.Manual;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Services.Authentication.Manual
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserResolverService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //public async Task<ApplicationUser> GetUserFromToken()
        //{
        //    var username = _httpContextAccessor.HttpContext.User.Identity.Name;

        //    //var username = "ali@dishiq.com"; // Hardcode your username here
        //    var applicationUser = await _userManager.FindByNameAsync(username);
        //    return applicationUser;
        //}

        public async Task<ApplicationUser> GetUserFromToken()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null)
            {
                throw new Exception("Username claim not found in token");
            }

            var applicationUser = await _userManager.FindByNameAsync(username);
            return applicationUser;
        }
    }
}
