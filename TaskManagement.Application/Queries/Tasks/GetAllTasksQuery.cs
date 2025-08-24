using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Queries.Tasks
{
    public class GetAllTasksQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? CurrentUserId { get; set; }
    }

}
