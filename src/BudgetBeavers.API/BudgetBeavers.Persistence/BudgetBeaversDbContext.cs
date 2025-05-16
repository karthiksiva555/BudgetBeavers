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

        modelBuilder.Entity<TransactionCategory>().HasData(
            new TransactionCategory { Id = Guid.Parse("7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b"), Name = "Groceries", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), Name = "Entertainment", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("b2c1d3e4-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), Name = "Food", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("c3d4e5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8"), Name = "Mortgage / Rent", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("d4e5f6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9"), Name = "Insurance", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("f7a8b9c0-d1e2-f3a4-b5c6-d7e8f9a0b1c2"), Name = "Utilities", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("b3c4d5e6-f7a8-b9c0-d1e2-f3a4b5c6d7e8"), Name = "Bill Payments", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), Name = "Loan Payments", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"), Name = "Unplanned Expense", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("f2a3b4c5-d6e7-f8a9-b0c1-d2e3f4a5b6c7"), Name = "Transportation", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("e1f2a3b4-c5d6-e7f8-a9b0-c1d2e3f4a5b6"), Name = "Welness", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("d8e9f0a1-b2c3-d4e5-f6a7-b8c9d0e1f2a3"), Name = "Shopping", CreatedAt = createdAt },
            new TransactionCategory { Id = Guid.Parse("c5d6e7f8-a9b0-c1d2-e3f4-a5b6c7d8e9f0"), Name = "Maintenance", CreatedAt = createdAt }
        );
    }
}