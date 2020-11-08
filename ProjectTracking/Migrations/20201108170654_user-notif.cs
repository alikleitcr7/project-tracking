using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usernotif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSheetId",
                table: "UserNotifications",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "475e8dbf-155f-4029-b8da-6b0cc6ee7bfb", "AQAAAAEAACcQAAAAEKF0V09E0quijjgiwGA3IIXO/iESRoskdzjefGYy5CY0YNqLo/L2bJsoV0wv+cb6gw==", new DateTime(2020, 11, 8, 19, 6, 53, 687, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "35f9c332-d296-4a76-b5e6-09cd955f4681", "AQAAAAEAACcQAAAAEPURXYzIlHvUJxmYATcgbZdVanXA0eQcoa0eimFKeQbjw3nLOPZPfpZ19y1Sql4PkA==", new DateTime(2020, 11, 8, 19, 6, 53, 697, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "4ff270b3-5ce2-4697-91d5-67b468aae629", "AQAAAAEAACcQAAAAEM5mq6Y30eySgXzk8Gi6p6YOqOYWYkpABvjZrOwG5PDVGN1/nprkPRkuYRRjnNuG3g==", new DateTime(2020, 11, 8, 19, 6, 53, 726, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d2866659-ceb0-48f6-bbb8-8c64d0ef1720", "AQAAAAEAACcQAAAAEKJRJGyJ1CJkwBBX8ctHowQAtJrbUc2JxilJSGKLGaPHDfhuZo4KNgmhASp8UpE9zA==", new DateTime(2020, 11, 8, 19, 6, 53, 733, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f98e55bd-35ba-4734-a8dc-7402d0b823e9", "AQAAAAEAACcQAAAAENDGy0wLUBMpIZb/PP7hvV5L/jbOPQFOsu7ZMsc1YQPA5FJa+zKWEvxMijtVdZ0n+A==", new DateTime(2020, 11, 8, 19, 6, 53, 740, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "69cf3f60-ee23-4b75-8105-8f587df014ae", "AQAAAAEAACcQAAAAEO0WqLJf8vtoa9535WOKGtjwbdXM4IXw29489m785EKWZB9XATStWhAJ/4Tga8jEAA==", new DateTime(2020, 11, 8, 19, 6, 53, 746, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c3ff9545-215d-4c4d-be47-cf25c567d46c", "AQAAAAEAACcQAAAAEEtTDiD17MamPS0UUJ0FpdxxblIahXOfxUT9BZ3XfSie0+ugKt+DrVWDuLYatWAB2w==", new DateTime(2020, 11, 8, 19, 6, 53, 705, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5bc99ae1-f872-4833-9c22-b63e0a8c738e", "AQAAAAEAACcQAAAAENVkKIoaetkbynNXTWvRv86PTwksBX7DblxN4gBWqu23L07VB0P0xrYMBsvOAChi4w==", new DateTime(2020, 11, 8, 19, 6, 53, 716, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_TimeSheetId",
                table: "UserNotifications",
                column: "TimeSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications",
                column: "TimeSheetId",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_TimeSheetId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "TimeSheetId",
                table: "UserNotifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e119b367-892b-4d0f-94ba-be9d35b5f142", "AQAAAAEAACcQAAAAEHVFdT+UdxOBopnZv167AmKpLKqThGZpbbYnjN807MmonFvTBJjp39NFEBDdv9vkgw==", new DateTime(2020, 10, 25, 12, 31, 20, 862, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "20bed9f1-ee1b-48f2-98c9-a129dace667c", "AQAAAAEAACcQAAAAEFMa5VivxuhLXc2DOpwjr9tYJMl3yZrHwQLsZoFN71TAzMkA4tPoP3e0eFeVPLdgGg==", new DateTime(2020, 10, 25, 12, 31, 20, 871, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "55ef2185-d01e-431e-9700-14508570d5fe", "AQAAAAEAACcQAAAAEL4wsy7cMHQ4BZu3hyVwKX5ch/ASDz7kEgCISOv1CtyuuyxtRrlfJgTBVN7fH7CyIw==", new DateTime(2020, 10, 25, 12, 31, 20, 900, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2d18183a-5e70-406a-8401-c80c691b4ba0", "AQAAAAEAACcQAAAAENAgCJiMy51kXZNSvGn9OjEY61NrLrrxP8ar+aVK7gchI3FcVN37RnyVMtrRV7WTzw==", new DateTime(2020, 10, 25, 12, 31, 20, 910, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e373897f-feb7-48e0-9f77-52dc97fc453c", "AQAAAAEAACcQAAAAEJEuBJ8bp2JMle8WGVYIh2e7tdbaSHnkjZP+QZ9MjNaoDgvq+8fEzNxpnV1z8Xuzww==", new DateTime(2020, 10, 25, 12, 31, 20, 918, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "19041f96-de53-4ecb-ba2a-4f3e397034b2", "AQAAAAEAACcQAAAAEDzAIwDx1obsVrq3y7RPwPID54OwyMqTlZz2IGkidbc1TrlEAUrJobD+GDMouYtJvA==", new DateTime(2020, 10, 25, 12, 31, 20, 927, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d46b3a95-c83b-4a10-9de0-edcdb62e0953", "AQAAAAEAACcQAAAAEOF0vU5scJnOxdU5d6qyPOnorHmCdxOVn7ON/MRe7xMV8GSRLrqyd3usLAu9r2H8pw==", new DateTime(2020, 10, 25, 12, 31, 20, 879, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "8ea3364a-aaf2-471e-b671-c01b2a278012", "AQAAAAEAACcQAAAAELQdK0UCfV/uSH2KYONZPeCfyashleWxeU30mhU2nuCEE9N8cWyTWG9Lwqopw6crTg==", new DateTime(2020, 10, 25, 12, 31, 20, 889, DateTimeKind.Local) });
        }
    }
}
