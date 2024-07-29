﻿namespace EarlyRetirement.Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }
}