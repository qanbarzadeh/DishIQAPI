using Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentication.Manual
{
    public interface IUserResolverService
    {
        Task<ApplicationUser> GetUserFromToken();
    }

}
