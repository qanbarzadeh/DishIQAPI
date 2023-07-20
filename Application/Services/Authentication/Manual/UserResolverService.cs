using Application.Interfaces.Authentication.Manual;
using Application.Interfaces.UserRepo;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Authentication.Manual
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public UserResolverService(IHttpContextAccessor httpContextAccessor, IApplicationUserRepository applicationUserRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<ApplicationUser> GetUserFromToken()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId == null)
            {
                return null;
            }

            return await _applicationUserRepository.GetUserByIdAsync(int.Parse(userId));
        }

    }
}
