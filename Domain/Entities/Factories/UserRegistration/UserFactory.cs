using Domain.Entities.UserRegistration;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserFactory
    {
        public static AuthUser CreateUser(string emailAddress, string username)
        {
            return new AuthUser
            {
                Id = Guid.NewGuid(), // assuming a new Guid for each new user
                EmailAddress = emailAddress,
                Username = username,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                UpdatedAt = DateTime.UtcNow,
                Version = 1,
                ExternalLogins = new List<ExternalLogin>()
            };
        }
    }
}
