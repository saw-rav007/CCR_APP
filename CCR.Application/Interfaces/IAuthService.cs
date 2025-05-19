using CCR.Application.DTOs;

namespace CCR.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
