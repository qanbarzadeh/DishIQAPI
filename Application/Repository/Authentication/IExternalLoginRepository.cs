using Domain.Entities.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Authentication
{
    public interface IExternalLoginRepository
    {
        Task<ExternalLogin> AddExternalLoginAsync(ExternalLogin externalLogin);
        Task<IEnumerable<ExternalLogin>> GetExternalLoginsByAuthUserIdAsync(Guid authUserId);     
    }
}
