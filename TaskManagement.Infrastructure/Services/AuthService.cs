using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Services;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IPasswordService passwordService, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email)
                ?? throw new UnauthorizedAccessException("Invalid email or password");

            if (!_passwordService.VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:TokenExpiryMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterAsync(string fullName, string email, string password, string role)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null) throw new ArgumentException("Email already registered");

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = _passwordService.HashPassword(password),
                Role = Enum.Parse<Role>(role)
            };
            await _userRepository.AddAsync(user);
            // Save changes handled by the calling service or controller
        }
    }
}