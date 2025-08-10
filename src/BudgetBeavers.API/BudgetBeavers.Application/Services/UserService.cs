using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class UserService(IUserRepository userRepository, IPasswordService passwordService): IUserService
{
    public async Task<UserDto> AddAsync(CreateUserDto createUserDto)
    {
        Guard.AgainstNull(createUserDto);
        Guard.AgainstNullOrWhiteSpace(createUserDto.FirstName);
        Guard.AgainstNullOrWhiteSpace(createUserDto.LastName);
        Guard.AgainstNullOrWhiteSpace(createUserDto.Email);
        Guard.AgainstNullOrWhiteSpace(createUserDto.Password);
        
        var user = createUserDto.ToEntity();
        user.PasswordHash = passwordService.HashPassword(createUserDto.Password);
        
        var createdUser = await userRepository.AddAsync(user);
        return createdUser.ToDto();
    }

    public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateUserDto)
    {
        Guard.AgainstNull(updateUserDto);
        Guard.AgainstEmptyGuid(id);
        Guard.AgainstNullOrWhiteSpace(updateUserDto.FirstName);
        Guard.AgainstNullOrWhiteSpace(updateUserDto.LastName);
        
        var existingUser = await userRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(existingUser, id);
        
        existingUser.FirstName = updateUserDto.FirstName;
        existingUser.LastName = updateUserDto.LastName;
        existingUser.PhoneNumber = updateUserDto.PhoneNumber;
        
        await userRepository.UpdateAsync(existingUser);
        
        return existingUser.ToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        var user = await userRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(user, id);
        
        await userRepository.DeleteAsync(user);
    }

    public async ValueTask<UserDto?> GetByIdAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        var user = await userRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(user, id);
        
        return user.ToDto();
    }
}