using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Persistence.Repositories;

public abstract class RepositoryBase<TEntity>(BudgetBeaversDbContext budgetBeaversDb) : IRepository<TEntity> where TEntity : BaseEntity
{
    public ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        return budgetBeaversDb.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await budgetBeaversDb.Set<TEntity>().AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        budgetBeaversDb.Set<TEntity>().Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        budgetBeaversDb.Set<TEntity>().Remove(entity);
        await SaveAsync();
    }

    protected virtual Task SaveAsync()
    {
        return budgetBeaversDb.SaveChangesAsync();
    }
}