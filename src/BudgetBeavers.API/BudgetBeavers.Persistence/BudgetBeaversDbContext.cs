using BudgetBeavers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetBeavers.Persistence;

public class BudgetBeaversDbContext(DbContextOptions<BudgetBeaversDbContext> options) : DbContext(options)
{
    public DbSet<Home> Homes => Set<Home>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
}