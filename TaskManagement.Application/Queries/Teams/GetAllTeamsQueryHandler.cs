using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Teams
{
    public class GetAllTeamsQueryHandler
    {
        private readonly ITeamRepository _teamRepository;

        public GetAllTeamsQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IList<Team>> Handle(GetAllTeamsQuery query, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetAllAsync(cancellationToken);
        }
    }

}
