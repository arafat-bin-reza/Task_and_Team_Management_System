using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Add this to use IConfiguration
using System;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration; // Add this field

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); // Inject IConfiguration
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var token = await _authService.LoginAsync(model.Email, model.Password);
            return Ok(new
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:TokenExpiryMinutes"]))
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _authService.RegisterAsync(model.FullName, model.Email, model.Password, model.Role);
            return Ok(new { Message = "User registered successfully" });
        }
    }
}