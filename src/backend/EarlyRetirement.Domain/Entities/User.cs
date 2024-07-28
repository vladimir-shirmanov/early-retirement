namespace EarlyRetirement.Domain.Entities;

public class User
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }
}