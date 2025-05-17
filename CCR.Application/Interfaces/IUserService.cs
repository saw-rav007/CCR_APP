using CCR.Application.DTOs;

namespace CCR.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(RegisterUserRequest request);
}
