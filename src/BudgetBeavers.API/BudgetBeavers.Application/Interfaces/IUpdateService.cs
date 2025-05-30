namespace BudgetBeavers.Application.Interfaces;

public interface IUpdateService<in T> where T : class
{
    Task UpdateAsync(T updateHomeDto);
}