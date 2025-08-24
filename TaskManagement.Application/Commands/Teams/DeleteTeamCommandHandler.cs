using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Teams
{
    public class DeleteTeamCommandHandler
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(command.TeamId, cancellationToken);
            if (team == null) throw new Exception("Team not found");

            await _teamRepository.DeleteAsync(team, cancellationToken);
        }
    }

}
