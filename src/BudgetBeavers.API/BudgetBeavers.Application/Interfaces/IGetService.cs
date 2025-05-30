namespace BudgetBeavers.Application.Interfaces;

public interface IGetService<T> where T : class
{
    ValueTask<T?> GetByIdAsync(Guid id);
}