using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Interfaces;

public interface IHomeService : ICrudService<CreateHomeDto, UpdateHomeDto, HomeDto>;