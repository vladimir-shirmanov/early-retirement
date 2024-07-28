using EarlyRetirement.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace EarlyRetirement.Infrastructure;

public class EfRepository<T> : IRepository<T> where T: class
{
    private readonly EarlyRetirementDbContext _context;

    public EfRepository(EarlyRetirementDbContext context)
    {
        _context = context;
    } 

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var data = await _context.Set<T>().ToListAsync();
        return data;
    }

    public ValueTask<T?> GetByIdAsync(int id)
    {
        var data = _context.Set<T>();
        return data.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}