namespace BudgetBeavers.Core.Entities;

public class HomeUser : BaseEntity
{
    public Guid HomeId { get; set; }
    
    public Home? Home { get; set; }
    
    public Guid RoleId { get; set; }
    
    public Role? Role { get; set; }
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}