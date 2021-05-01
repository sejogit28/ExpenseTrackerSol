using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class intializedAndAddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8408bc4d-9c5f-4537-a987-3505fa5de2f4", "d9c3cd3b-1544-4d52-8e53-e85cdd69c525", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac23df9f-ae6c-4ec9-8982-b15b248631e9", "d3e07eac-1546-44f2-9158-0a83a4916109", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8408bc4d-9c5f-4537-a987-3505fa5de2f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac23df9f-ae6c-4ec9-8982-b15b248631e9");
        }
    }
}
