namespace EarlyRetirement.Api.UserManagement.Models;

public interface IValidatable
{
    Dictionary<string, string[]> Validate();
}