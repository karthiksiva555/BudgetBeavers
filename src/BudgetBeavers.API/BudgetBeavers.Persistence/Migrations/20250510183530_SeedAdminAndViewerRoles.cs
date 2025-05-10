using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetBeavers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAndViewerRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("e0a3e06a-5e88-4f57-9481-35ab6c71205a"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Admin" },
                    { new Guid("f4b1c4a9-0f93-4d0a-a7b7-cd3cbe58f937"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Viewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e0a3e06a-5e88-4f57-9481-35ab6c71205a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4b1c4a9-0f93-4d0a-a7b7-cd3cbe58f937"));
        }
    }
}
