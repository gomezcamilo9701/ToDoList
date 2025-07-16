using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto.User;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserDto loginDto)
        {
            try
            {
                var response = await _userService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}