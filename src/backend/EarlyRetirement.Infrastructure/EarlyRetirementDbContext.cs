using EarlyRetirement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EarlyRetirement.Infrastructure;

public class EarlyRetirementDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public EarlyRetirementDbContext(DbContextOptions<EarlyRetirementDbContext> options) : base(options)
    {
    }
}