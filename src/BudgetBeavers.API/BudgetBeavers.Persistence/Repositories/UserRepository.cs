using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;

namespace BudgetBeavers.Persistence.Repositories;

public class UserRepository(BudgetBeaversDbContext budgetBeaversDb): RepositoryBase<User>(budgetBeaversDb),IUserRepository;