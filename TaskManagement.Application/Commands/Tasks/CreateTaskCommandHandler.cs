using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Tasks
{
    public class CreateTaskCommandHandler
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = new TaskEntity
            {
                Title = command.Title,
                Description = command.Description,
                AssignedToUserId = command.AssignedToUserId,
                CreatedByUserId = command.CreatedByUserId,
                DueDate = command.DueDate,
                Status = Domain.Enums.TaskStatus.Todo
            };

            await _taskRepository.AddAsync(task, cancellationToken);
            return task.Id;
        }
    }
}
