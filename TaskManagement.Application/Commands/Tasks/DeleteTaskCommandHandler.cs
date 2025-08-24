using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Tasks
{
    public class DeleteTaskCommandHandler
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task == null) throw new Exception("Task not found");

            await _taskRepository.DeleteAsync(task, cancellationToken);
        }
    }

}
