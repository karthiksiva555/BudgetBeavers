using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Core.Interfaces;

public interface IHomeUserRepository : IRepository<HomeUser>
{
    Task<IEnumerable<User>> GetMembersByHomeIdAsync(Guid homeId);
}