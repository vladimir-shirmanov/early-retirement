namespace EarlyRetirement.Domain.Core;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    ValueTask<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}