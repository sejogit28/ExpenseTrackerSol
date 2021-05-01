using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class madeSmallChangesToIncomesTableAndManyChangesToExpenseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Incomes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePaid",
                table: "Incomes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Expenses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MonthYear",
                table: "Expenses",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DatePaid",
                table: "Incomes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MonthYear",
                table: "Expenses",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
