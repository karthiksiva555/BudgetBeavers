using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeUserService(IHomeUserRepository homeUserRepository): IHomeUserService
{
    public async Task<HomeUserDto> AddAsync(CreateHomeUserDto createHomeUserDto)
    {
        Guard.AgainstNull(createHomeUserDto, nameof(createHomeUserDto));
        Guard.AgainstEmptyGuid(createHomeUserDto.HomeId, nameof(createHomeUserDto.HomeId));
        Guard.AgainstEmptyGuid(createHomeUserDto.UserId, nameof(createHomeUserDto.UserId));
        Guard.AgainstEmptyGuid(createHomeUserDto.RoleId, nameof(createHomeUserDto.RoleId));
        
        var homeUser = createHomeUserDto.ToEntity();
        var createdHomeUser = await homeUserRepository.AddAsync(homeUser);
        return createdHomeUser.ToDto();
    }

    public async Task<HomeUserDto> UpdateAsync(Guid id, UpdateHomeUserDto updateHomeUserDto)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        Guard.AgainstNull(updateHomeUserDto, nameof(updateHomeUserDto));
        Guard.AgainstEmptyGuid(updateHomeUserDto.UserId, nameof(updateHomeUserDto.UserId));
        Guard.AgainstEmptyGuid(updateHomeUserDto.RoleId, nameof(updateHomeUserDto.RoleId));
        
        var existingHomeUser = await homeUserRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(existingHomeUser, id, nameof(id));
        
        existingHomeUser.UserId = updateHomeUserDto.UserId;
        existingHomeUser.RoleId = updateHomeUserDto.RoleId;
        await homeUserRepository.UpdateAsync(existingHomeUser);
        
        return existingHomeUser.ToDto();
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