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
            // Get the userId claim from the JWT token.
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            // Check if the userId claim is missing or not a valid integer.
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                // If the userId claim is missing or not a valid integer:
                // Log the error for further investigation or debugging purposes.
                // For example:
                _logger.LogError($"Invalid userId claim in JWT token. userIdClaim: '{userIdClaim}'");

                // Alternatively, you can return a default user or an anonymous user based on your application's requirements.
                // For example:
                // return _applicationUserRepository.GetAnonymousUser();

                // If you want to return an HTTP error response indicating unauthorized access, you can do the following:
                // You may need to import System;
                // throw new AuthenticationException("Invalid or missing userId claim in JWT token.");

                // In this case, let's just return null, indicating that the user cannot be resolved from the token.
                return null;
            }

            // If the userId claim is present and a valid integer, proceed to retrieve the user from the repository.
            return await _applicationUserRepository.GetUserByIdAsync(userId);
        }

    }
}
