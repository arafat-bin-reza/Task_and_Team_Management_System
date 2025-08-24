using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    namespace Infrastructure.Repositories
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly AppDbContext _context;
            private readonly DbSet<T> _dbSet;

            public Repository(AppDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }

            public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            {
                return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            }

            public async Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default)
            {
                return await _dbSet.ToListAsync(cancellationToken);
            }

            public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            {
                return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
            }

            public async Task<IPaginatedList<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, CancellationToken cancellationToken = default)
            {
                IQueryable<T> query = _dbSet;

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var totalCount = await query.CountAsync(cancellationToken);
                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                return new PaginatedList<T>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }

            public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            {
                await _dbSet.AddAsync(entity, cancellationToken);
            }

            public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
            {
                _dbSet.Update(entity);
                await Task.CompletedTask; // EF Core tracks changes, no async save needed here
            }

            public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
            {
                _dbSet.Remove(entity);
                await Task.CompletedTask; // EF Core tracks changes, no async save needed here
            }
        }

        public class PaginatedList<T> : IPaginatedList<T> where T : class
        {
            public IList<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
        }
    }
}
