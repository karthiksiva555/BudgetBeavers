using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeUserService(IHomeUserRepository homeUserRepository): IHomeUserService
{
    public Task<HomeUserDto> AddAsync(CreateHomeUserDto createUserDto)
    {
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