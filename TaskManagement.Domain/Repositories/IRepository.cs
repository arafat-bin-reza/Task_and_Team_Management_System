using System.Linq.Expressions;

namespace TaskManagement.Domain.Repositories
{
    public interface IRepository<T> where T : class
        {
            Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
            Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
            Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
            Task<IPaginatedList<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, CancellationToken cancellationToken = default);
            Task AddAsync(T entity, CancellationToken cancellationToken = default);
            Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
            Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        }

        public interface IPaginatedList<T> where T : class
        {
            IList<T> Items { get; }
            int TotalCount { get; }
            int PageNumber { get; }
            int PageSize { get; }
            int TotalPages { get; }
        }
}
