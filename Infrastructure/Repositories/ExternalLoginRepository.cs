using Application.Repository.Authentication;
using Domain.Entities.UserRegistration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExternalLoginRepository : IExternalLoginRepository
    {
        private readonly AppDbContext _context;

        public ExternalLoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExternalLogin> AddExternalLoginAsync(ExternalLogin externalLogin)
        {
            _context.ExternalLogins.Add(externalLogin);
            await _context.SaveChangesAsync();
            return externalLogin;
        }

        public async Task<IEnumerable<ExternalLogin>> GetExternalLoginsByAuthUserIdAsync(Guid authUserId)
        {
            return await _context.ExternalLogins
                                 .Where(el => el.AuthUserId == authUserId)
                                 .ToListAsync();
        }       
    }
}
