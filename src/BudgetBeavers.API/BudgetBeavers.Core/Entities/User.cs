namespace BudgetBeavers.Core.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public string? PhoneNumber { get; set; }
    
    public ICollection<HomeUser> HomeUsers { get; set; } = new List<HomeUser>();
}