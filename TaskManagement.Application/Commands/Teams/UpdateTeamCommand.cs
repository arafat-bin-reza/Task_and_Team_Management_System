using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Teams
{
    public class UpdateTeamCommand
    {
        public int TeamId { get; set; }
        public string? Name { get; set; }
    }

}
