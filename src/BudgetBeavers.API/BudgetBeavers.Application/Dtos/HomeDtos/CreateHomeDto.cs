using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Dtos.HomeDtos;

public class CreateHomeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}

public static class CreateHomeDtoExtensions
{
    public static Home ToEntity(this CreateHomeDto dto)
    {
        return new Home { Name = dto.Name, CreatedAt = DateTime.UtcNow };
    }
}