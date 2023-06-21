using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserFactory
    {
        public static User CreateUser(string emailAddress, string username)
        {
            return new User
            {
                EmailAddress = emailAddress,
                Username = username,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}