using BudgetBeavers.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BudgetBeavers.API.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetBeaversDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<BudgetBeaversDbContext>>();

        if (!dbContext.Database.IsRelational()) return;
        
        try
        {
            logger.LogInformation("Applying migrations to the database.");
            dbContext.Database.Migrate();
            logger.LogInformation("Migrations applied.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }
}