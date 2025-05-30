namespace BudgetBeavers.Application.Interfaces;

public interface IAddService<in T> where T : class
{
    Task AddAsync(T entity);
}