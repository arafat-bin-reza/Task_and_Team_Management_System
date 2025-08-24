using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Teams
{
    public class UpdateTeamCommandHandler
    {
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(command.TeamId, cancellationToken);
            if (team == null) throw new Exception("Team not found");

            if (!string.IsNullOrEmpty(command.Name)) team.Name = command.Name;

            await _teamRepository.UpdateAsync(team, cancellationToken);
        }
    }

}
