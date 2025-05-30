using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Dtos.HomeDtos;

public class UpdateHomeDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

public static class UpdateHomeDtoExtensions
{
    public static Home ToEntity(this UpdateHomeDto dto)
    {
        return new Home { Id = dto.Id, Name = dto.Name };
    }
}