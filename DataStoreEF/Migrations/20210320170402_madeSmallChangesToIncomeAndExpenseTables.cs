using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class madeSmallChangesToIncomeAndExpenseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Income");

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DatePaid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthYear = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncomeTypes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    IncomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthYear = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.IncomeId);
                });
        }
    }
}
