using EarlyRetirement.Api.Utils;
using EarlyRetirement.Application.Dtos;

namespace EarlyRetirement.Api.UserManagement.Models;

public class RegisterViewModel : IValidatable
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }

    public RegisterDto ToRegisterDto()
    {
        return new RegisterDto
        {
            Email = Email,
            Password = Password
        };
    }

    public Dictionary<string, string[]> Validate()
    {
        var problems = new Dictionary<string, string[]>();
        var emailValidationProblem = "Email is invalid";
        var passwordsDoNotMatch = "Passwords don't match";
        
        if (Email.EndsWith("."))
        {
            problems.Add(nameof(Email), [emailValidationProblem]);
        }
        
        // only return true if there is only 1 '@' character
        // and it is neither the first nor the last character
        int index = Email.IndexOf('@');
        if (index <= 0 ||
            index == Email.Length - 1 ||
            index != Email.LastIndexOf('@'))
        {
            if(!problems.ContainsKey(nameof(Email)))
                problems.Add(nameof(Email), [emailValidationProblem]);
        }

        if (Password != ConfirmPassword)
        {
            problems.Add(nameof(ConfirmPassword), [passwordsDoNotMatch]);
        }

        return problems;
    }
}