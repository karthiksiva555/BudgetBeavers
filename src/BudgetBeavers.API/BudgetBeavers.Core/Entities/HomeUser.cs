namespace BudgetBeavers.Core.Entities;

public class HomeUser : BaseEntity
{
    public Guid HomeId { get; set; }
    
    public required Home Home { get; set; }
    
    public Guid RoleId { get; set; }
    
    public required Role Role { get; set; }
    
    public Guid UserId { get; set; }
    
    public required User User { get; set; }
}