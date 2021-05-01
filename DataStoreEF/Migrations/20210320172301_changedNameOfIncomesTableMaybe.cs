using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class changedNameOfIncomesTableMaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePaid",
                table: "Incomes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePaid",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
