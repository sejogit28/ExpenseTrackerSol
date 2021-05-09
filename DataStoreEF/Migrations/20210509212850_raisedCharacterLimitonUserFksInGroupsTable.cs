using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class raisedCharacterLimitonUserFksInGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52cf1672-f4c6-4c88-8a54-4465c359aa84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "798bfdc1-ffb3-469e-854e-97fef506eb01");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Incomes",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "Groups",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d394f94c-8f4c-4a0e-816d-c59e184cd9b1", "c0e78147-f3a5-4d8c-a1fa-3deef5061715", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9899ce34-e7b1-4913-8aed-8bb089e98054", "9cbd27da-f2b2-4769-a50f-ba1b57ad87db", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9899ce34-e7b1-4913-8aed-8bb089e98054");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d394f94c-8f4c-4a0e-816d-c59e184cd9b1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Incomes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "798bfdc1-ffb3-469e-854e-97fef506eb01", "a8656535-8912-4b79-9f30-828798ae3621", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52cf1672-f4c6-4c88-8a54-4465c359aa84", "0fbea7e4-4aee-4847-820a-a3fb55d5e915", "Administrator", "ADMINISTRATOR" });
        }
    }
}
