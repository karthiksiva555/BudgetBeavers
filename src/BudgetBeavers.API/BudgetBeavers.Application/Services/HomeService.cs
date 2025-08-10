using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeService(IHomeRepository homeRepository) : IHomeService
{
    public async Task<HomeDto> AddAsync(CreateHomeDto createHomeDto)
    {
        Guard.AgainstNull(createHomeDto);
        Guard.AgainstNullOrWhiteSpace(createHomeDto.Name);

        var home = createHomeDto.ToEntity();
        var createdHome = await homeRepository.AddAsync(home);
        return createdHome.ToDto();
    }

    public async Task<HomeDto> UpdateAsync(Guid id, UpdateHomeDto updateHomeDto)
    {
        Guard.AgainstNull(updateHomeDto);
        Guard.AgainstNullOrWhiteSpace(updateHomeDto.Name);
        Guard.AgainstEmptyGuid(id);
        
        var existingHome = await homeRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(existingHome, id);
        
        existingHome.Name = updateHomeDto.Name;
        
        await homeRepository.UpdateAsync(existingHome);
        
        return existingHome.ToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        var home = await homeRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(home, id);
        
        await homeRepository.DeleteAsync(home);
    }

    public async ValueTask<HomeDto?> GetByIdAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id);
        var home = await homeRepository.GetByIdAsync(id);
        Guard.AgainstKeyNotFound(home, id);
        
        return home.ToDto();
    }
}