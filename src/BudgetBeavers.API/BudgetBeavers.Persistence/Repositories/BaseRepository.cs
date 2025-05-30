using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Persistence.Repositories;

public abstract class BaseRepository<TEntity>(BudgetBeaversDbContext budgetBeaversDb) : IRepository<TEntity> where TEntity : BaseEntity
{
    public ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        ValidateId(id);
        return budgetBeaversDb.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        ValidateEntity(entity);
        await budgetBeaversDb.Set<TEntity>().AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        ValidateEntity(entity);
        budgetBeaversDb.Set<TEntity>().Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        ValidateId(id);
        var entity = await budgetBeaversDb.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        budgetBeaversDb.Set<TEntity>().Remove(entity);
        await SaveAsync();
    }

    protected virtual void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }
    }
    
    protected virtual void ValidateEntity(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        
        if (entity.Id == Guid.Empty)
        {
            throw new ArgumentException("Entity ID cannot be empty.", nameof(entity));
        }
    }

    protected virtual Task SaveAsync()
    {
        return budgetBeaversDb.SaveChangesAsync();
    }
}