using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class initalMigrationForAzureDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9899ce34-e7b1-4913-8aed-8bb089e98054");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d394f94c-8f4c-4a0e-816d-c59e184cd9b1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fa65dd5-cbbf-460a-8904-e9e6ea52aa77", "050f977d-f745-41ca-90ac-1e9be6cdbfa6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc948cde-4e2c-49f1-802c-c69d1800c2a1", "2d2d0e07-d90c-4f62-9c5b-5ca8364858a4", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fa65dd5-cbbf-460a-8904-e9e6ea52aa77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc948cde-4e2c-49f1-802c-c69d1800c2a1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d394f94c-8f4c-4a0e-816d-c59e184cd9b1", "c0e78147-f3a5-4d8c-a1fa-3deef5061715", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9899ce34-e7b1-4913-8aed-8bb089e98054", "9cbd27da-f2b2-4769-a50f-ba1b57ad87db", "Administrator", "ADMINISTRATOR" });
        }
    }
}
