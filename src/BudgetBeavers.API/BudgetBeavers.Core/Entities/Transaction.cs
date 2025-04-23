namespace BudgetBeavers.Core.Entities;

public class Transaction : BaseEntity
{
    public Guid HomeId { get; set; }
    
    public required Home Home { get; set; }

    public Guid UserId { get; set; }
    
    public required User User { get; set; }

    public Guid TransactionCategoryId { get; set; }

    public required TransactionCategory TransactionCategory { get; set; }
    
    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow.Date;
}