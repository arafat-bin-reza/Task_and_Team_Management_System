using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Tasks
{
    public class GetTaskByIdQueryHandler
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetByIdAsync(query.TaskId, cancellationToken);
        }
    }


}
