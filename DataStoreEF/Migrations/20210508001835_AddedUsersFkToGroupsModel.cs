using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class AddedUsersFkToGroupsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e29576-0403-450e-ba13-0912cb780c55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e6f248-1f0c-4956-bf04-1bdaf46d8d7d");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseTrackerUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "798bfdc1-ffb3-469e-854e-97fef506eb01", "a8656535-8912-4b79-9f30-828798ae3621", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52cf1672-f4c6-4c88-8a54-4465c359aa84", "0fbea7e4-4aee-4847-820a-a3fb55d5e915", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ExpenseTrackerUserId",
                table: "Groups",
                column: "ExpenseTrackerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_ExpenseTrackerUserId",
                table: "Groups",
                column: "ExpenseTrackerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_ExpenseTrackerUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ExpenseTrackerUserId",
                table: "Groups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52cf1672-f4c6-4c88-8a54-4465c359aa84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "798bfdc1-ffb3-469e-854e-97fef506eb01");

            migrationBuilder.DropColumn(
                name: "ExpenseTrackerUserId",
                table: "Groups");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e6f248-1f0c-4956-bf04-1bdaf46d8d7d", "05df5a05-e3f0-42ef-9bf4-b8e1e655f2c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e29576-0403-450e-ba13-0912cb780c55", "c34ea7fd-d207-4c66-80ff-08913936e176", "Administrator", "ADMINISTRATOR" });
        }
    }
}
