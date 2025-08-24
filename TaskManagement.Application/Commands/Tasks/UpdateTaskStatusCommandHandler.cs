using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Tasks
{
    public class UpdateTaskStatusCommandHandler
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskStatusCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(UpdateTaskStatusCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task == null) throw new Exception("Task not found");

            task.Status = command.Status;
            await _taskRepository.UpdateAsync(task, cancellationToken);
        }
    }

}
