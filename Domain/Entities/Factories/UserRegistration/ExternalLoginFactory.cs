using Domain.Entities.UserRegistration;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class ExternalLoginFactory
    {
        public static ExternalLogin CreateExternalLogin(string loginProvider, string providerKey, AuthUser user)
        {
            return new ExternalLogin
            {
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                AuthUser = user,
                AuthUserId = user.Id,
                LinkedAt = DateTime.UtcNow
            };
        }
    }
}
