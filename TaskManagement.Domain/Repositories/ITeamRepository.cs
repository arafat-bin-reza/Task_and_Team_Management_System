using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<IList<Team>> GetTeamsWithMembersAsync(CancellationToken cancellationToken = default);
    }
}
