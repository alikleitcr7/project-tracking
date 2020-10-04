using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class commentsrenameanduserlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "UserLogging");

            migrationBuilder.DropColumn(
                name: "TimeSheetProjectTaskId",
                table: "TimeSheetActivityLogs");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "TimeSheetActivityLogs",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "TimeSheetActivities",
                newName: "Message");

            migrationBuilder.AddColumn<short>(
                name: "LogStatusCode",
                table: "UserLogging",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "d1c2c2f0-18c5-4911-9939-c55321802e8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "05fb03eb-11e1-4af4-b870-3d4081516964");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ec39bba9-d916-4ee4-88a2-74e5e47fb63d", "AQAAAAEAACcQAAAAEOnxoq8/rq3HmpmYmH0+lIZlhgYMkIP3Zxyc+A6IaPctTsFL+86cG6i17JSz0dOhwA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01802a18-3dec-4296-9c7f-e77e076afbd8", "AQAAAAEAACcQAAAAEKxv/Ohv7Agm8iJEq/jtMJ14FuXrH6ASrehsc+/BfNWDT9KxSL2kt8w0tIcGwN8xVA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogStatusCode",
                table: "UserLogging");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "TimeSheetActivityLogs",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "TimeSheetActivities",
                newName: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "UserLogging",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeSheetProjectTaskId",
                table: "TimeSheetActivityLogs",
                nullable: false,
                defaultValue: 0);

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
    }
}
