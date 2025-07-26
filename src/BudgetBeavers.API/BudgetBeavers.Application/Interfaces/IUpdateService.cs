namespace BudgetBeavers.Application.Interfaces;

public interface IUpdateService<in TUpdateDto, TDto> 
    where TUpdateDto : class
    where TDto : class
{
    Task<TDto> UpdateAsync(Guid id, TUpdateDto updateHomeDto);
}