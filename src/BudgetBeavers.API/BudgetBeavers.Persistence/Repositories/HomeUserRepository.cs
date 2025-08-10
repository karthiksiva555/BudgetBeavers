using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetBeavers.Persistence.Repositories;

public class HomeUserRepository(BudgetBeaversDbContext budgetBeaversDb) : RepositoryBase<HomeUser>(budgetBeaversDb), IHomeUserRepository
{
    private readonly BudgetBeaversDbContext _budgetBeaversDb = budgetBeaversDb;

    public Task<IEnumerable<User?>> GetMembersByHomeIdAsync(Guid homeId)
    {
        return _budgetBeaversDb.HomeUsers
            .Where(hu => hu.HomeId == homeId)
            .Select(hu => hu.User)
            .ToListAsync()
            .ContinueWith(task => task.Result.AsEnumerable());
    }
}