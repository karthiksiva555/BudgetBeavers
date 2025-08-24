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
        
        var existingUser = await userRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(existingUser, id);
        
        if (updateUserDto.FirstName is null && updateUserDto.LastName is null && updateUserDto.Email is null && updateUserDto.PhoneNumber is null)
            throw new ArgumentException("At least one field must be provided for update.", nameof(updateUserDto));
        
        existingUser.FirstName = updateUserDto.FirstName ?? existingUser.FirstName;
        existingUser.LastName = updateUserDto.LastName ?? existingUser.LastName;
        existingUser.Email = updateUserDto.Email ?? existingUser.Email;
        existingUser.PhoneNumber = updateUserDto.PhoneNumber ?? existingUser.PhoneNumber;
        
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