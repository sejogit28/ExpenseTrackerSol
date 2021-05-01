using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class CreatedInitialDataBaseWithNoIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DateSpent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    IncomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.IncomeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Income");
        }
    }
}
