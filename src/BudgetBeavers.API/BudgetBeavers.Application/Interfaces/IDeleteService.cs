namespace BudgetBeavers.Application.Interfaces;

public interface IDeleteService
{
    Task DeleteAsync(Guid id);
}