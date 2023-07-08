using Application.Repository.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthUserRepository AuthUsers { get; }
        
        
        Task SaveChangesAsync();
    }
}
