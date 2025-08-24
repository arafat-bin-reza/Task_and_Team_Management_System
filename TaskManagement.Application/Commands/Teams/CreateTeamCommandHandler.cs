using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Teams
{
    public class CreateTeamCommandHandler
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<int> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Name = command.Name
            };

            await _teamRepository.AddAsync(team, cancellationToken);
            return team.Id;
        }
    }

}
