using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class appuserremovedcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTracked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "ec4ee308-b322-4b97-93b1-60e0c27a8b2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "e8752d03-188c-4d02-ba5b-7b9275f484da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d37eef6-53bb-4b75-af12-09550af04a0a", "AQAAAAEAACcQAAAAEKR2HjXmFwiD8erfw6GcLG7O1KBilcabmcNGvGdEnBv+/obGaLRnpR/h++lxkIZCxQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "15056b09-365c-494a-8848-0b66324d44d1", "AQAAAAEAACcQAAAAED3YT+3iPzsXfxeqqVotR67vvSg7hEpGMi29UP8DSw/6WZrdIB28fN9J9JrYD2uHrg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTracked",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "c42370a9-b084-4be8-8ea2-c28d69f47124");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "1cac7be6-e2ed-4fd4-a884-c59509329f98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId" },
                values: new object[] { "bd2feb22-8ba7-485c-a511-01fab8709a1b", "AQAAAAEAACcQAAAAEBWCPexUHkz/NqCbo3M0uP0pD8Q4jA0uMsyu9pfaSEy0FZFsrGbqQyqPXedCQF8t7w==", "a18be9c0-aa65-4af8-bd17-00bd9344e572" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId" },
                values: new object[] { "371e13f6-bc05-43d1-a187-bf01108e67a9", "AQAAAAEAACcQAAAAEFrSHo7Hdn4AmacxKp/OGqOCNXCj/7G8xJ5LNms5b44krvjgUiFYPEV6aC+P8TPaAQ==", "a18be9c0-aa65-4af8-bd17-00bd9344e590" });
        }
    }
}
