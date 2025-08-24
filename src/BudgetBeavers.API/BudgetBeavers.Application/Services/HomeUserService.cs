using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeUserService(IHomeUserRepository homeUserRepository): IHomeUserService
{
    public async Task<HomeUserDto> AddAsync(CreateHomeUserDto createHomeUserDto)
    {
        Guard.AgainstNull(createHomeUserDto);
        Guard.AgainstEmptyGuid(createHomeUserDto.HomeId);
        Guard.AgainstEmptyGuid(createHomeUserDto.UserId);
        Guard.AgainstEmptyGuid(createHomeUserDto.RoleId);
        
        var homeUser = createHomeUserDto.ToEntity();
        var createdHomeUser = await homeUserRepository.AddAsync(homeUser);
        return createdHomeUser.ToDto();
    }

    public async Task<HomeUserDto> UpdateAsync(Guid id, UpdateHomeUserDto updateHomeUserDto)
    {
        Guard.AgainstEmptyGuid(id);
        Guard.AgainstNull(updateHomeUserDto);
        if (updateHomeUserDto.RoleId is null && updateHomeUserDto.UserId is null)
        {
            throw new ArgumentException("At least one property (RoleId or UserId) must be provided for update.", nameof(updateHomeUserDto));
        }
        
        var existingHomeUser = await homeUserRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(existingHomeUser, id);

        existingHomeUser.UserId = updateHomeUserDto.UserId ?? existingHomeUser.UserId;
        existingHomeUser.RoleId = updateHomeUserDto.RoleId ?? existingHomeUser.RoleId;
        await homeUserRepository.UpdateAsync(existingHomeUser);
        
        return existingHomeUser.ToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        
        var homeUser = await homeUserRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(homeUser, id);
        
        await homeUserRepository.DeleteAsync(homeUser);
    }

    public async ValueTask<HomeUserDto?> GetByIdAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        
        var homeUser = await homeUserRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(homeUser, id);
        
        return homeUser.ToDto();
    }

    public async Task<IEnumerable<UserDto>> GetMembersByHomeIdAsync(Guid homeId)
    {
        Guard.AgainstEmptyGuid(homeId);
        
        var users = await homeUserRepository.GetMembersByHomeIdAsync(homeId);
        
        return users.Where(u => u != null).Select(u => u!.ToDto());
    }
}