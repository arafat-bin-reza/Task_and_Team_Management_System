using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Teams
{
    public class GetTeamByIdQueryHandler
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team?> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetByIdAsync(query.TeamId, cancellationToken);
        }
    }

}
