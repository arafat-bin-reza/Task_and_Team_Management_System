using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Commands.Tasks
{
    public class UpdateTaskStatusCommand
    {
        public int TaskId { get; set; }
        public Domain.Enums.TaskStatus Status { get; set; }
    }

}
