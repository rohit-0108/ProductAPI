using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public UserService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _unitOfWork.Repository<User>().GetAllAsync();
        if (existingUser.Any(u => u.Username == request.Username))
            return false;

        var user = new User
        {
            Username = request.Username,
            // In a real app, use BCrypt or ASP.NET Identity to hash. 
            // For this assessment, we'll do a simple mock hash.
            PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Password)),
            CreatedBy = "System"
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        var user = users.FirstOrDefault(u => u.Username == request.Username);

        if (user == null) return null;

        var inputHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Password));
        if (user.PasswordHash != inputHash) return null;

        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username, user.Role);

        return new LoginResponse
        {
            Token = token,
            Username = user.Username
        };
    }
}
