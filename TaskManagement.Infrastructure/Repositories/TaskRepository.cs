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
        public class TaskRepository : Repository<TaskEntity>, ITaskRepository
        {

        private readonly AppDbContext _context;
        private readonly DbSet<TaskEntity> _dbSet;

        public TaskRepository(AppDbContext context) : base(context)  
        {
            _context = context;
            _dbSet = context.Set<TaskEntity>(); // this makes _dbSet available
        }

            public async Task<IList<TaskEntity>> GetTasksByUserIdAsync(int userId, CancellationToken cancellationToken = default)
            {
                return await _dbSet
                    .Where(t => t.AssignedToUserId == userId || t.CreatedByUserId == userId)
                    .ToListAsync(cancellationToken);
            }

            public async Task<IList<TaskEntity>> GetTasksByTeamIdAsync(int teamId, CancellationToken cancellationToken = default)
            {
                return await _dbSet
                    .Where(t => t.TeamId == teamId)
                    .ToListAsync(cancellationToken);
            }
        }
}
