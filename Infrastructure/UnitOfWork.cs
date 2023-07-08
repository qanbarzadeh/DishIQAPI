using Application.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Removed the IExternalLoginRepository and IUserEventRepository dependencies.

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
