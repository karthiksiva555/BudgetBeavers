using BudgetBeavers.Application.Dtos.HomeUserDtos;

namespace BudgetBeavers.Application.Interfaces;

public interface IHomeUserService : ICrudService<CreateHomeUserDto, UpdateHomeUserDto, HomeUserDto>
{
    Task<IEnumerable<HomeUserDto>> GetMembersByHomeIdAsync(Guid homeId);
}