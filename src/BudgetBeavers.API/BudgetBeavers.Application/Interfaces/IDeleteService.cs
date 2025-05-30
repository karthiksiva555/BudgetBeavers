namespace BudgetBeavers.Application.Interfaces;

public interface IDeleteService<T> where T : class
{
    Task DeleteAsync(Guid id);
}