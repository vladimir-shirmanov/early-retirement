using System.Security.Cryptography;
using System.Text;
using EarlyRetirement.Application.Options;
using Microsoft.Extensions.Options;

namespace EarlyRetirement.Application.Services;

public interface IHashStrategy
{
    string HashPassword(string data);
    ValueTask<string> HashPasswordAsync(string data);
}

public class Sha512HashStrategy(IOptions<SecurityConfiguration> secConfig) : IHashStrategy
{
    private readonly SecurityConfiguration _secConfig = secConfig.Value;
    public string HashPassword(string data)
    {
        using var sha512 = SHA512.Create();
        var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes($"{data}{_secConfig.Salt}"));
        return Encoding.UTF8.GetString(hashBytes);
    }

    public ValueTask<string> HashPasswordAsync(string data)
    {
        throw new NotImplementedException();
    }
}