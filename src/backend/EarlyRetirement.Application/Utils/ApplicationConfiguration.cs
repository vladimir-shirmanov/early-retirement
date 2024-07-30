using EarlyRetirement.Application.Options;
using EarlyRetirement.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EarlyRetirement.Application.Utils;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddEarlyRetirementLogic(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<SecurityOptions>(configuration.GetSection(nameof(SecurityOptions)));
        collection.AddScoped<IUserManager, UserManager>();
        collection.AddScoped<IHashStrategy, Sha512HashStrategy>();
        return collection;
    }
}