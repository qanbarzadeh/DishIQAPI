using Application.Repository.Authentication;
using Domain.Entities.UserRegistration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AppDbContext _context;

        public AuthUserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<AuthUser> AddAuthUserAsync(AuthUser authUser)
        {
            if (authUser == null)
                throw new ArgumentNullException(nameof(authUser));

            _context.AuthUsers.Add(authUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // You can add more specific error handling here, if needed
                throw new Exception("Failed to save new user to the database", ex);
            }

            return authUser;
        }

        public async Task<AuthUser> GetAuthUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            try
            {
                return await _context.AuthUsers.FirstOrDefaultAsync(u => u.EmailAddress == email);
            }
            catch (Exception ex)
            {
                // You can add more specific error handling here, if needed
                throw new Exception("Failed to retrieve user from the database", ex);
            }
        }
    }
}
