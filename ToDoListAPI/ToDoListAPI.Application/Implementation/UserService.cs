using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto.User;
using ToDoListAPI.Domain.Models;
using ToDoListAPI.Infrastructure;

namespace ToDoListAPI.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly ToDoListCamiloGomezContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ToDoListCamiloGomezContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && !u.IsDeleted);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return new LoginResponseDto
            {
                Token = token,
                User = new UserDto { Id = user.Id, Username = user.Username }
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username && !u.IsDeleted))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var user = new Users
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto { Id = user.Id, Username = user.Username };
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
