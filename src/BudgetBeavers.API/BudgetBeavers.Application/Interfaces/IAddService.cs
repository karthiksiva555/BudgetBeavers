namespace BudgetBeavers.Application.Interfaces;

public interface IAddService<in TCreateDto, THomeDto> where TCreateDto : class where THomeDto : class
{
    Task<THomeDto> AddAsync(TCreateDto createHomeDto);
}