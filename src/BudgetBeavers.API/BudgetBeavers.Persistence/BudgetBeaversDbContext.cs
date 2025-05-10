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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var createdAt = new DateTime(2025, 05, 10, 12, 0, 0, DateTimeKind.Utc); 
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.Parse("e0a3e06a-5e88-4f57-9481-35ab6c71205a"), Name = "Admin", CreatedAt = createdAt },
            new Role { Id = Guid.Parse("f4b1c4a9-0f93-4d0a-a7b7-cd3cbe58f937"), Name = "Viewer", CreatedAt = createdAt }
        );
    }
}