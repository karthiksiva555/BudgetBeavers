namespace BudgetBeavers.Core.Interfaces;

public interface IRepository<T> where T : class
{
    ValueTask<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}