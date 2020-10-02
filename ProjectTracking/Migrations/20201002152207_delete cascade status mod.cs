using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class deletecascadestatusmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "50531e26-5e6b-4c31-82f0-3c6564c8c2cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "aad0253c-368d-449e-9693-d81b953cef98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1e1c7103-028e-4fa4-9ad5-d8cdcdb272cd", "AQAAAAEAACcQAAAAEB/zTBZ0ubhlbcYOTgHOxZ7X9FkirzXFaytyJIU0X7bo78xlIEAidXP+5+T+q5ZdzQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "d603061c-73f7-446d-9c0d-2475210ec106");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "e6d9f923-fc0a-4a7c-bbdc-45ee39a9727f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7da13084-cdea-4d27-a061-596694c15e7c", "AQAAAAEAACcQAAAAECC+7COLbJ5gmzNpP/6rJBsf+2QT5TbRZ/P3cBzfk7EvlYWwBFgB2AQSf+nMxx4HxQ==" });
        }
    }
}
