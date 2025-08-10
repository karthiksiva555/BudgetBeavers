using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeUserService(IHomeUserRepository homeUserRepository): IHomeUserService
{
    public Task<HomeUserDto> AddAsync(CreateHomeUserDto createHomeUserDto)
    {
        Guard.AgainstNull(createHomeUserDto, nameof(createHomeUserDto));
        Guard.AgainstEmptyGuid(createHomeUserDto.HomeId, nameof(createHomeUserDto.HomeId));
        Guard.AgainstEmptyGuid(createHomeUserDto.UserId, nameof(createHomeUserDto.UserId));
        Guard.AgainstEmptyGuid(createHomeUserDto.RoleId, nameof(createHomeUserDto.RoleId));
        throw new NotImplementedException();
    }

    public Task<HomeUserDto> UpdateAsync(Guid id, UpdateHomeUserDto updateHomeDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<HomeUserDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HomeUserDto>> GetMembersByHomeIdAsync(Guid homeId)
    {
        throw new NotImplementedException();
    }
}