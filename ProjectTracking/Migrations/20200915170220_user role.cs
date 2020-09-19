using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class userrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "eef09435-f554-4d4f-9702-cd418976eabb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e590", "fa896e3b-4fb7-4d3f-9e14-d6f391f69a58", "IdentityRole", "User", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d19f3a1e-9c26-4ddf-8bdf-c41647ecc502", "AQAAAAEAACcQAAAAEIZY5Jpoks9KdhzFQmNhM8zkMZ1XCn4d645CrWmN7Q20f53jKB8mKXtQRupqwIBy9Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e590", "fa896e3b-4fb7-4d3f-9e14-d6f391f69a58" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "99869ee7-da61-4e81-9f9a-a242a5a071f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9e0b10b-b788-409a-ba44-0521adeeade4", "AQAAAAEAACcQAAAAENwUSxzSqTKHEEuO1Ts4RjOjcLlk3oHo5c2sYZipovl8UCsH8e3p4QVmkwGRWv01Sw==" });
        }
    }
}
