using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EarlyRetirement.Infrastructure;

public class DbContextFactory : IDesignTimeDbContextFactory<EarlyRetirementDbContext>
{
    public EarlyRetirementDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<EarlyRetirementDbContext>();
        optionBuilder.UseNpgsql(args.First());
        return new EarlyRetirementDbContext(optionBuilder.Options);
    }
}