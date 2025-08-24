using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagement.Application.Commands.Teams;
using TaskManagement.Application.Queries.Teams;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllTeamsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var teams = await _mediator.Send(query);
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetTeamByIdQuery { TeamId = id };
            var team = await _mediator.Send(query);
            if (team == null) return NotFound(new { Message = $"Team with ID {id} not found" });
            return Ok(team);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var team = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamCommand command)
        //{
        //    if (id != command.Id) return BadRequest(new { Message = "ID mismatch" });
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTeamCommand { TeamId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}