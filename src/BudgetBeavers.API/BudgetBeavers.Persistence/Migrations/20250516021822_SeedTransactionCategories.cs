using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetBeavers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedTransactionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Entertainment" },
                    { new Guid("7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Groceries" },
                    { new Guid("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Loan Payments" },
                    { new Guid("b2c1d3e4-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Food" },
                    { new Guid("b3c4d5e6-f7a8-b9c0-d1e2-f3a4b5c6d7e8"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Bill Payments" },
                    { new Guid("c3d4e5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Mortgage / Rent" },
                    { new Guid("c5d6e7f8-a9b0-c1d2-e3f4-a5b6c7d8e9f0"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Maintenance" },
                    { new Guid("d4e5f6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Insurance" },
                    { new Guid("d8e9f0a1-b2c3-d4e5-f6a7-b8c9d0e1f2a3"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Shopping" },
                    { new Guid("e1f2a3b4-c5d6-e7f8-a9b0-c1d2e3f4a5b6"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Welness" },
                    { new Guid("e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Unplanned Expense" },
                    { new Guid("f2a3b4c5-d6e7-f8a9-b0c1-d2e3f4a5b6c7"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Transportation" },
                    { new Guid("f7a8b9c0-d1e2-f3a4-b5c6-d7e8f9a0b1c2"), new DateTime(2025, 5, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Utilities" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("b2c1d3e4-5f6a-7b8c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("b3c4d5e6-f7a8-b9c0-d1e2-f3a4b5c6d7e8"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("c5d6e7f8-a9b0-c1d2-e3f4-a5b6c7d8e9f0"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("d8e9f0a1-b2c3-d4e5-f6a7-b8c9d0e1f2a3"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-e7f8-a9b0-c1d2e3f4a5b6"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("f2a3b4c5-d6e7-f8a9-b0c1-d2e3f4a5b6c7"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("f7a8b9c0-d1e2-f3a4-b5c6-d7e8f9a0b1c2"));
        }
    }
}
