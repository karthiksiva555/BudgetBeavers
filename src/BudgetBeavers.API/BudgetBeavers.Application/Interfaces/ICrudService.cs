namespace BudgetBeavers.Application.Interfaces;

public interface ICrudService<in TCreateDto, in TUpdateDto, TGetDto> : IAddService<TCreateDto, TGetDto>, IUpdateService<TUpdateDto, TGetDto>,
    IDeleteService, IGetService<TGetDto>
    where TCreateDto : class
    where TUpdateDto : class
    where TGetDto : class;