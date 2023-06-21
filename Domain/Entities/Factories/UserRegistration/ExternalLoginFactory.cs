using Domain.Entities.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                User = user,
                UserId = user.Id,
                LinkedAt = DateTime.UtcNow
            };
        }
    }
}
