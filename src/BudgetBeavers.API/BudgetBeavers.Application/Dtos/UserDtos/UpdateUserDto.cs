namespace BudgetBeavers.Application.Dtos.UserDtos;

public class UpdateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string? PhoneNumber { get; set; }
}