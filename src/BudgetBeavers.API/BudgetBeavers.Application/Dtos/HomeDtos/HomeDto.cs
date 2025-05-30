namespace BudgetBeavers.Application.Dtos.HomeDtos;

public class HomeDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

public static class HomeDtoExtensions
{
    public static HomeDto ToDto(this Core.Entities.Home home)
    {
        return new HomeDto { Id = home.Id, Name = home.Name };
    }
}