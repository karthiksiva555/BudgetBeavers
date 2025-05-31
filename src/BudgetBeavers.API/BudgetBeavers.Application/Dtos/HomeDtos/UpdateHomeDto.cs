using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Dtos.HomeDtos;

public class UpdateHomeDto
{
    public required string Name { get; set; }
}

public static class UpdateHomeDtoExtensions
{
    public static Home ToEntity(this UpdateHomeDto dto)
    {
        return new Home { Name = dto.Name };
    }
}