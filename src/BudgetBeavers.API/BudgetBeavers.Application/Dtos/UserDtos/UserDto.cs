namespace BudgetBeavers.Application.Dtos.UserDtos;

public class UserDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required DateTime CreatedAt { get; set; }
}