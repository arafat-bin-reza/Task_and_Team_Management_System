using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Application.Queries.Tasks;
using TaskManagement.Application.Commands.Teams;
using TaskManagement.Application.Queries.Teams;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string status = null, [FromQuery] int? assignedTo = null, [FromQuery] int? teamId = null,
            [FromQuery] string dueDate = null)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetAllTasksQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Status = status,
                AssignedToUserId = assignedTo,
                TeamId = teamId,
                DueDate = string.IsNullOrEmpty(dueDate) ? (DateTime?)null : DateTime.Parse(dueDate),
                CurrentUserId = userId
            };
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetTaskByIdQuery { TaskId = id };
            var task = await _mediator.Send(query);
            if (task == null) return NotFound(new { Message = $"Task with ID {id} not found" });
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (task.AssignedToUserId.ToString() != userId && User.IsInRole("Employee"))
            //    return Forbid(new { Message = "Employees can only view their own tasks" });
            return Ok(task);
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        //public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    command.CreatedByUserId = int.Parse(userId);
        //    var task = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskCommand command)
        //{
        //    if (id != command.Id) return BadRequest(new { Message = "ID mismatch" });
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    command.CurrentUserId = userId;
        //    //if (int.Parse(userId) != command.AssignedToUserId && User.IsInRole("Employee") && !string.IsNullOrEmpty(command.Status))
        //    //    return Forbid(new { Message = "Employees can only update their own task status" });
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTaskCommand { TaskId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}