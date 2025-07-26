namespace BudgetBeavers.Core.Entities;

public class Home : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<HomeUser> HomeUsers { get; set; } = new List<HomeUser>();
}