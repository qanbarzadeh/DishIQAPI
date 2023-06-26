using Application.Interfaces.UnitOfWork;
using Application.Repository.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAuthUserRepository AuthUsers { get; }
        public IExternalLoginRepository ExternalLogins { get; }
        public IUserEventRepository UserEvents { get; }

        public UnitOfWork(AppDbContext context, IAuthUserRepository authUsers, IExternalLoginRepository externalLogins, IUserEventRepository userEvents)
        {
            _context = context;
            AuthUsers = authUsers;
            ExternalLogins = externalLogins;
            UserEvents = userEvents;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
