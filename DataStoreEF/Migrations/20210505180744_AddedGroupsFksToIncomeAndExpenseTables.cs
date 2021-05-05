using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTrackerApi.Migrations
{
    public partial class AddedGroupsFksToIncomeAndExpenseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupsGroupId",
                table: "GroupUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9f465c5-9e95-4020-9d5b-f9fce97fcc6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aed00e20-9ec8-4c78-bd75-cb569c0e9175");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupId",
                table: "GroupUsers",
                newName: "GroupsGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_GroupsGroupId",
                table: "GroupUsers",
                newName: "IX_GroupUsers_GroupsGroupsId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "GroupsId");

            migrationBuilder.AddColumn<int>(
                name: "GroupsGroupsId",
                table: "Incomes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupsGroupsId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e6f248-1f0c-4956-bf04-1bdaf46d8d7d", "05df5a05-e3f0-42ef-9bf4-b8e1e655f2c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e29576-0403-450e-ba13-0912cb780c55", "c34ea7fd-d207-4c66-80ff-08913936e176", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_GroupsGroupsId",
                table: "Incomes",
                column: "GroupsGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupsGroupsId",
                table: "Expenses",
                column: "GroupsGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Groups_GroupsGroupsId",
                table: "Expenses",
                column: "GroupsGroupsId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupsGroupsId",
                table: "GroupUsers",
                column: "GroupsGroupsId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Groups_GroupsGroupsId",
                table: "Incomes",
                column: "GroupsGroupsId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Groups_GroupsGroupsId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupsGroupsId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Groups_GroupsGroupsId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_GroupsGroupsId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_GroupsGroupsId",
                table: "Expenses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e29576-0403-450e-ba13-0912cb780c55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e6f248-1f0c-4956-bf04-1bdaf46d8d7d");

            migrationBuilder.DropColumn(
                name: "GroupsGroupsId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "GroupsGroupsId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupsId",
                table: "GroupUsers",
                newName: "GroupsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_GroupsGroupsId",
                table: "GroupUsers",
                newName: "IX_GroupUsers_GroupsGroupId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9f465c5-9e95-4020-9d5b-f9fce97fcc6a", "b26791c5-87b5-474c-8bd3-6706532cb85b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aed00e20-9ec8-4c78-bd75-cb569c0e9175", "ec80e424-7977-44cc-bf1f-5a0fdf05c6e5", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupsGroupId",
                table: "GroupUsers",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
