using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Dtos.UserDtos;

public class CreateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}

public static class CreateUserDtoExtensions
{
    public static User ToEntity(this CreateUserDto createUserDto)
    {
        return new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            PhoneNumber = createUserDto.PhoneNumber
        };
    }
}