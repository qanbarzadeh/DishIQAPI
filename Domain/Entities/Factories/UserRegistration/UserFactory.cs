using Domain.Entities.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserFactory
    {
        public static AuthUser CreateUser(string emailAddress, string username)
        {
            return new AuthUser
            {
                EmailAddress = emailAddress,
                Username = username,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}