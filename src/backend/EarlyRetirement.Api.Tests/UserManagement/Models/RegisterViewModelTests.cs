using EarlyRetirement.Api.UserManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EarlyRetirement.Api.Tests.UserManagement.Models;

public class RegisterViewModelTests
{
    [Test]
    public void Validate_PassValidEmail_ShouldReturnEmpyList()
    {
        var registerModel = new RegisterViewModel
        {
            Email = "vova@mail.com",
            Password = "123",
            ConfirmPassword = "123"
        };

        var result = registerModel.Validate();

        Assert.That(result, Is.Empty);
    }
    
    [TestCase("@vovaemail")]
    [TestCase("vova@email@other.com")]
    [TestCase("vovaEmail@")]
    [TestCase("vovaEmail")]
    public void Validate_PassInValidEmail_ShouldReturnEmailProblem(string email)
    {
        var registerModel = new RegisterViewModel
        {
            Email = email,
            Password = "123",
            ConfirmPassword = "123"
        };

        var result = registerModel.Validate();

        Assert.That(result.ContainsKey(nameof(registerModel.Email)));
    }
    
    [Test]
    public void Validate_PassInValidEmailWithTwoErrors_ShouldReturnOnlyOneEmailProblem()
    {
        var registerModel = new RegisterViewModel
        {
            Email = "@vova.",
            Password = "123",
            ConfirmPassword = "123"
        };

        var result = registerModel.Validate();

        Assert.That(result, Has.One.Matches<KeyValuePair<string, string[]>>(i => i.Key == nameof(registerModel.Email)));
    }

    [Test]
    public void ToRegisterDto_ShouldCorrectlyMap_ToRegisterDto()
    {
        var registerModel = new RegisterViewModel
        {
            Email = "@vova.",
            Password = "123",
            ConfirmPassword = "123"
        };

        var result = registerModel.ToRegisterDto();
        
        Assert.That(result.Email, Is.EqualTo(registerModel.Email));
        Assert.That(result.Password, Is.EqualTo(registerModel.Password));
    }
    
    [Test]
    public void Validate_PassDifferentPasswords_ShouldReturnConfirmPasswordProblem()
    {
        var registerModel = new RegisterViewModel
        {
            Email = "@vova.",
            Password = "123",
            ConfirmPassword = "124"
        };

        var result = registerModel.Validate();

        Assert.That(result.ContainsKey(nameof(registerModel.ConfirmPassword)));
    }
}