using EarlyRetirement.Application.Dtos;
using EarlyRetirement.Application.Services;
using EarlyRetirement.Domain.Core;
using EarlyRetirement.Domain.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EarlyRetirement.Application.Tests.Services;

[TestFixture]
public class UserManagerTests
{
    private IRepository<User> _repository;
    private IHashStrategy _hashStrategy;
    private ILogger<UserManager> _logger;
    private UserManager _userManager;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IRepository<User>>();
        _hashStrategy = Substitute.For<IHashStrategy>();
        _logger = Substitute.For<ILogger<UserManager>>();
        _userManager = new UserManager(_repository, _hashStrategy, _logger);
    }

    [Test]
    public async Task RegisterUserAsync_PassUserData_ShouldHashPassword()
    {
        var user = new RegisterDto
        {
            Email = "test@mail.com",
            Password = "abc"
        };

        await _userManager.RegisterUserAsync(user);

        _hashStrategy.Received(1).HashPassword(user.Password);
    }

    [Test]
    public async Task RegisterUserAsync_PassUserData_ShouldSaveToDatabase()
    {
        var user = new RegisterDto
        {
            Email = "test@mail.com",
            Password = "abc"
        };

        await _userManager.RegisterUserAsync(user);

        await _repository.Received(1).AddAsync(Arg.Is<User>(u => u.Email == user.Email));
    }

    [Test]
    public async Task RegisterUserAsync_PassUserData_ShouldLogRegistered()
    {
        var user = new RegisterDto
        {
            Email = "test@mail.com",
            Password = "abc"
        };

        await _userManager.RegisterUserAsync(user);
    }
}