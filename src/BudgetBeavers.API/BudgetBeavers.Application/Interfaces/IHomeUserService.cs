using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Dtos.UserDtos;

namespace BudgetBeavers.Application.Interfaces;

public interface IHomeUserService : ICrudService<CreateHomeUserDto, UpdateHomeUserDto, HomeUserDto>
{
    Task<IEnumerable<UserDto>> GetMembersByHomeIdAsync(Guid homeId);
}