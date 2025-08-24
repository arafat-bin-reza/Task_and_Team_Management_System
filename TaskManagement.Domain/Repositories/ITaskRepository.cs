using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task AddAsync(TaskEntity task, CancellationToken cancellationToken = default);
        Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<TaskEntity>> GetTasksByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);
    }
}
