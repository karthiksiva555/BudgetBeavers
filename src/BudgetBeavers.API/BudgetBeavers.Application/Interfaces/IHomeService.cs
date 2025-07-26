using BudgetBeavers.Application.Dtos.HomeDtos;

namespace BudgetBeavers.Application.Interfaces;

public interface IHomeService : ICrudService<CreateHomeDto, UpdateHomeDto, HomeDto>;