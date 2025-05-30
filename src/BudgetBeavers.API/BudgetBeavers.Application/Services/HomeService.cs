using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Utilities;
using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Application.Services;

public class HomeService(IHomeRepository homeRepository) : IHomeService
{
    public Task AddAsync(Home home)
    {
        Guard.AgainstNull(home, nameof(home));
        Guard.AgainstNullOrWhiteSpace(home.Name, nameof(home.Name));
        
        return homeRepository.AddAsync(home);
    }

    public Task UpdateAsync(Home home)
    {
        Guard.AgainstNull(home, nameof(home));
        Guard.AgainstNullOrWhiteSpace(home.Name, nameof(home.Name));
        Guard.AgainstEmptyGuid(home.Id, nameof(home.Id));
        
        return homeRepository.UpdateAsync(home);
    }

    public Task DeleteAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        
        return homeRepository.DeleteAsync(id);
    }

    public ValueTask<Home?> GetByIdAsync(Guid id)
    {
        Guard.AgainstEmptyGuid(id, nameof(id));
        
        return homeRepository.GetByIdAsync(id);
    }
}