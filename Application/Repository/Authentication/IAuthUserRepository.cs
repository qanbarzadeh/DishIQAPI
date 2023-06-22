using Domain.Entities.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Authentication
{
    public interface IAuthUserRepository
    {
        Task<AuthUser> AddAuthUserAsync(AuthUser authUser);
        Task<AuthUser> GetAuthUserByEmailAsync(string email);

    }
}
