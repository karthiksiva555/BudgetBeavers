namespace BudgetBeavers.Application.Dtos.HomeUserDtos;

public class CreateHomeUserDto
{
    public Guid HomeId { get; set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
}