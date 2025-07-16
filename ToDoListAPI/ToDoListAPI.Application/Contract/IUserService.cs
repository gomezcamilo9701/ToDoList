using ToDoListAPI.Application.Dto.User;

namespace ToDoListAPI.Application.Contract
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginUserDto loginDto);
        Task<UserDto> RegisterAsync(RegisterUserDto registerDto);
    }
}
