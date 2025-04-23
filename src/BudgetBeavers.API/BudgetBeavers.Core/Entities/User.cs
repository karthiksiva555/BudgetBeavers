namespace BudgetBeavers.Core.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;

    public Guid HomeId { get; set; }
    
    public required Home Home { get; set; }

    public Guid RoleId { get; set; }

    public required Role Role { get; set; }
}