using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Repositories.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.Repositories
{
        public class TeamRepository : Repository<Team>, ITeamRepository
        {
        private readonly AppDbContext _context;
        private readonly DbSet<Team> _dbSet;
        public TeamRepository(AppDbContext context) : base(context) 
        {
            _context = context;
            _dbSet = context.Set<Team>(); // this makes _dbSet available
        }

            public async Task<IList<Team>> GetTeamsWithMembersAsync(CancellationToken cancellationToken = default)
            {
                return await _dbSet
                    .Include(t => t.Members)
                    .ToListAsync(cancellationToken);
            }
        }
}
