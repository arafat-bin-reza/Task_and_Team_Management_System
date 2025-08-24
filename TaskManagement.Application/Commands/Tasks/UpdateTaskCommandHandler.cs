using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Tasks
{
    public class UpdateTaskCommandHandler
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task == null) throw new Exception("Task not found");

            if (!string.IsNullOrEmpty(command.Title)) task.Title = command.Title;
            if (!string.IsNullOrEmpty(command.Description)) task.Description = command.Description;
            if (command.DueDate.HasValue) task.DueDate = command.DueDate.Value;

            await _taskRepository.UpdateAsync(task, cancellationToken);
        }
    }
}
