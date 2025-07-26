using BudgetBeavers.Application.Dtos.UserDtos;

namespace BudgetBeavers.Application.Interfaces;

public interface IUserService : ICrudService<CreateUserDto, UpdateUserDto, UserDto>;