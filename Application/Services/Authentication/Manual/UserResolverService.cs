using Application.Interfaces.Authentication.Manual;
using Application.Interfaces.UserRepo;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; // Import the ILogger namespace
using System.Security.Claims;

namespace Application.Services.Authentication.Manual
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ILogger<UserResolverService> _logger; // Add ILogger dependency

        public UserResolverService(IHttpContextAccessor httpContextAccessor, IApplicationUserRepository applicationUserRepository, ILogger<UserResolverService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationUserRepository = applicationUserRepository;
            _logger = logger; // Inject the ILogger
        }

        public async Task<ApplicationUser> GetUserFromToken()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError($"Invalid or missing userId claim in JWT token. userIdClaim: '{userId}'");
                return null;
            }

            try
            {
                return await _applicationUserRepository.GetUserByIdAsync(userId);
            }
            catch (KeyNotFoundException knfex)
            {
                _logger.LogError(knfex, $"User with userId {userId} not found in the database.");
                return null;
            }
        }


    }
}
