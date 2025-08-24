using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Tasks
{
    public class GetTasksByUserQueryHandler
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksByUserQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IList<TaskEntity>> Handle(GetTasksByUserQuery query, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetTasksByUserIdAsync(query.UserId, cancellationToken);
        }
    }

}
