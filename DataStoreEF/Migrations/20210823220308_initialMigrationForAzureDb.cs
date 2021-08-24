using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class initialMigrationForAzureDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "217b2a69-c61c-4009-b9e2-39ca651c9b40", "e4487b59-eb82-44db-b687-82be33cef511", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "098172bb-53fb-41b2-b3a1-ac67477c70f7", "017bd7fb-9ee3-4506-a9b5-0ec5c369bfea", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "098172bb-53fb-41b2-b3a1-ac67477c70f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "217b2a69-c61c-4009-b9e2-39ca651c9b40");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fa65dd5-cbbf-460a-8904-e9e6ea52aa77", "050f977d-f745-41ca-90ac-1e9be6cdbfa6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc948cde-4e2c-49f1-802c-c69d1800c2a1", "2d2d0e07-d90c-4f62-9c5b-5ca8364858a4", "Administrator", "ADMINISTRATOR" });
        }
    }
}
