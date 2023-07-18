using Application.Interfaces.Authentication.Manual;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

        public async Task<ApplicationUser> GetUserFromToken()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var applicationUser = await _userManager.FindByNameAsync(username);

            return applicationUser;
        }
    }
}
