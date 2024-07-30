using EarlyRetirement.Application.Dtos;
using EarlyRetirement.Domain.Core;
using EarlyRetirement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EarlyRetirement.Application.Services;

public interface IUserManager
{
    Task RegisterUserAsync(RegisterDto model);
}

public class UserManager(IRepository<User> userRepository,
    IHashStrategy hashStrategy,
    ILogger<UserManager> logger) : IUserManager
{
    public async Task RegisterUserAsync(RegisterDto model)
    {
        string hashedPassword = hashStrategy.HashPassword(model.Password);
        var user = new User
        {
            Email = model.Email,
            PasswordHash = hashedPassword
        };
        await userRepository.AddAsync(user);
        logger.LogInformation("User Registered {@User}", user);
    }
}