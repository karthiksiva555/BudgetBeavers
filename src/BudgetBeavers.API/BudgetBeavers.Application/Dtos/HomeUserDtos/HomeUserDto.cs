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

public static class HomeUserDtoExtensions
{
    public static HomeUserDto ToDto(this Core.Entities.HomeUser homeUser)
    {
        return new HomeUserDto
        {
            Id = homeUser.Id,
            HomeId = homeUser.HomeId,
            HomeName = homeUser.Home?.Name ?? string.Empty,
            RoleId = homeUser.RoleId,
            RoleName = homeUser.Role?.Name ?? string.Empty,
            UserId = homeUser.UserId,
            UserFullName = $"{homeUser.User?.FirstName} {homeUser.User?.LastName}".Trim()
        };
    }
}