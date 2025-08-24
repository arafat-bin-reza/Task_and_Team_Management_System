using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagement.Application.Commands.Users;
using TaskManagement.Application.Queries.Users;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllUsersQuery { PageNumber = pageNumber, PageSize = pageSize };
            var users = await _mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery { UserId = id };
            var user = await _mediator.Send(query);
            if (user == null) return NotFound(new { Message = $"User with ID {id} not found" });
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var user = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        //{
        //    if (id != command.Id) return BadRequest(new { Message = "ID mismatch" });
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand { UserId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}