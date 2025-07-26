namespace BudgetBeavers.Application.Interfaces;

public interface IAddService<in TCreateDto, TDto> where TCreateDto : class where TDto : class
{
    Task<TDto> AddAsync(TCreateDto createUserDto);
}