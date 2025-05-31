namespace BudgetBeavers.Application.Interfaces;

public interface IUpdateService<in TUpdateHomeDto, THomeDto> 
    where TUpdateHomeDto : class
    where THomeDto : class
{
    Task<THomeDto> UpdateAsync(Guid id, TUpdateHomeDto updateHomeDto);
}