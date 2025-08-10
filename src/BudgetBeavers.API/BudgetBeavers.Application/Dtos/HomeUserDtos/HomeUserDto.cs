namespace BudgetBeavers.Application.Dtos.HomeUserDtos;

public class HomeUserDto
{
    public Guid Id { get; set; }
    public Guid HomeId { get; set; }
    public string HomeName { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
}