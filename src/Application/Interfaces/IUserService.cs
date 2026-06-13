using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
