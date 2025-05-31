using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Persistence.Repositories;

public class HomeRepository(BudgetBeaversDbContext budgetBeaversDb) : RepositoryBase<Home>(budgetBeaversDb), IHomeRepository;