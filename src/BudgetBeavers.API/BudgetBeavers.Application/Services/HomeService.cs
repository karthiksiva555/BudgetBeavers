using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeService(IHomeRepository homeRepository) : IHomeService
{
    public async Task<HomeDto> AddAsync(CreateHomeDto createHomeDto)
    {
        Guard.AgainstNull(createHomeDto, nameof(createHomeDto));
        Guard.AgainstNullOrWhiteSpace(createHomeDto.Name, nameof(createHomeDto.Name));

        var home = createHomeDto.ToEntity();
        var createdHome = await homeRepository.AddAsync(home);
        return createdHome.ToDto();
    }

    public Task UpdateAsync(UpdateHomeDto updateHomeDto)
    {
        Guard.AgainstNull(updateHomeDto, nameof(updateHomeDto));
        Guard.AgainstNullOrWhiteSpace(updateHomeDto.Name, nameof(updateHomeDto.Name));
        Guard.AgainstEmptyGuid(updateHomeDto.Id, nameof(updateHomeDto.Id));
        
        var home = updateHomeDto.ToEntity();
        return homeRepository.UpdateAsync(home);
    }

    public Task DeleteAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        
        return homeRepository.DeleteAsync(id);
    }

    public async ValueTask<HomeDto?> GetByIdAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        
        var home = await homeRepository.GetByIdAsync(id);
        if (home == null)
        {
            throw new KeyNotFoundException($"Home not found with the provided ID {id}.");
        }
        
        return home.ToDto();
    }
}