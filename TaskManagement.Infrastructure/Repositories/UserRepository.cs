using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Repositories.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
        {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(AppDbContext context) : base(context) 
        {
            _context = context;
            _dbSet = context.Set<User>(); // this makes _dbSet available
        }

            public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            {
                return await _dbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            }
        }
}
