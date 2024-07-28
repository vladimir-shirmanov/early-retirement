using EarlyRetirement.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EarlyRetirement.Infrastructure;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddRetirementDb(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<EarlyRetirementDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("EarlyRetirement")));
        collection.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        return collection;
    }
}