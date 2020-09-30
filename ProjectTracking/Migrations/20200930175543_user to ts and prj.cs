using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usertotsandprj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "TimeSheets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddedByUserId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId1",
                table: "Projects",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "9dbd257d-373d-45b7-aac2-3064301aa100");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "d24e9816-a990-4abc-b57a-233e84481de7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7f225ef-a9c2-4ced-9885-cf546d9290aa", "AQAAAAEAACcQAAAAEMW1wEhmLewdayM4Ihxuf1Uf9hVeo92lB0ZaSrFNL7QMyYI7YUV+IbkXHeb0v10a1g==" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_AddedByUserId",
                table: "TimeSheets",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedByUserId1",
                table: "Projects",
                column: "AddedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId1",
                table: "Projects",
                column: "AddedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_AspNetUsers_AddedByUserId",
                table: "TimeSheets",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_AspNetUsers_AddedByUserId",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheets_AddedByUserId",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AddedByUserId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AddedByUserId1",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "625471f2-7829-49e8-9173-24fba8313161");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "adb5e88d-124d-496e-9ce9-f77b5c5a1e86");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "83028c0f-6643-4361-a834-98a0ae21f8d8", "AQAAAAEAACcQAAAAEOWbjsCXtQNQB8S8fuKQ7djrEcNbEcoWIOCQXANLunZlEBhJX6dxAg9jedbD0W7pgg==" });
        }
    }
}
