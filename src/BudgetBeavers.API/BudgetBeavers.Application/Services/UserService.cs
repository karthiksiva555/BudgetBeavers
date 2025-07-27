using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class UserService(IUserRepository userRepository, IPasswordService passwordService): IUserService
{
    public async Task<UserDto> AddAsync(CreateUserDto createUserDto)
    {
        Guard.AgainstNull(createUserDto, nameof(createUserDto));
        Guard.AgainstNullOrWhiteSpace(createUserDto.FirstName, nameof(createUserDto.FirstName));
        Guard.AgainstNullOrWhiteSpace(createUserDto.LastName, nameof(createUserDto.LastName));
        Guard.AgainstNullOrWhiteSpace(createUserDto.Email, nameof(createUserDto.Email));
        Guard.AgainstNullOrWhiteSpace(createUserDto.Password, nameof(createUserDto.Password));
        
        var user = createUserDto.ToEntity();
        user.PasswordHash = passwordService.HashPassword(createUserDto.Password);
        
        var createdUser = await userRepository.AddAsync(user);
        return createdUser.ToDto();
    }

    public Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateUserDto)
    {
        Guard.AgainstNull(updateUserDto, nameof(updateUserDto));
        Guard.AgainstEmptyGuid(id, nameof(id));
        Guard.AgainstNullOrWhiteSpace(updateUserDto.FirstName, nameof(updateUserDto.FirstName));
        Guard.AgainstNullOrWhiteSpace(updateUserDto.LastName, nameof(updateUserDto.LastName));
        
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