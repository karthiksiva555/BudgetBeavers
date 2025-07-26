using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class UserService(IUserRepository userRepository): IUserService
{
    public Task<UserDto> AddAsync(CreateUserDto createHomeDto)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateHomeDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}