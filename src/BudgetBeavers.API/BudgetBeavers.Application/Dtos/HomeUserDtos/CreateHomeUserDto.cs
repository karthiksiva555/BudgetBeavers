using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Dtos.HomeUserDtos;

public class CreateHomeUserDto
{
    public Guid HomeId { get; set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
}

public static class CreateHomeUserDtoExtensions
{
    public static HomeUser ToEntity(this CreateHomeUserDto dto)
    {
        return new HomeUser
        {
            HomeId = dto.HomeId,
            RoleId = dto.RoleId,
            UserId = dto.UserId
        };
    }
}