using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class UserNotifRefDeleteSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "3f061004-8d81-40d3-aaa9-7c4173dfc30d", "AQAAAAEAACcQAAAAEHBJYzpidfGX76wAHfY4uBHUAxtapdjFV+TDviOSIx6lQ/msm6K9Cr2plClzyWTh2A==", new DateTime(2020, 11, 10, 14, 54, 8, 197, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "30bd433b-a370-4fc2-bfb7-95b9ac22b8e4", "AQAAAAEAACcQAAAAEMvrydHiN7dSEbOYSfOkO788GE3waCXViDsJIHcLSfJKYXoVgBGLP/qhMRwMzLoaww==", new DateTime(2020, 11, 10, 14, 54, 8, 206, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "07194737-f9fd-4964-a5bb-b2bd8125f250", "AQAAAAEAACcQAAAAEFQzC3opIuGDXUUCHgH79wqlA76/6nfquM82uqy/aDX88ITs6RNx3eSqSMUW+oQvfg==", new DateTime(2020, 11, 10, 14, 54, 8, 226, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d2b0f547-e7ca-47b5-b258-8029ca26d84d", "AQAAAAEAACcQAAAAEB9GvvvGAJF3ZNlvzBu70Pzzdgl60CDUtI8X2ieM6KhknT1Mz4URrZP0pUMUhh67/Q==", new DateTime(2020, 11, 10, 14, 54, 8, 233, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "da68049b-86ea-46ee-b018-17bd27c6b3a6", "AQAAAAEAACcQAAAAELz8G1OLqE6BesewOJtqKKo45RYZo1546pQI23zcg5Toh3GW6cbOGbRAnVhEUKBplw==", new DateTime(2020, 11, 10, 14, 54, 8, 239, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "653fc455-48a5-4244-81c1-b80ab0b043c3", "AQAAAAEAACcQAAAAEGaMshop+Q8RXXCwWBLyWcKqxuVJdxxhUgL1ysm4yZeunozJBmiFHnhVRHVNEPbVzA==", new DateTime(2020, 11, 10, 14, 54, 8, 246, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5ea1feb9-297f-4d23-bb5a-3f2750f3a2de", "AQAAAAEAACcQAAAAELWugyhKhUL7+34O42eRUu/Ws8IAuWM0ctXTFgQMYUL/A1k8IruySaFHuF3AIX6RIQ==", new DateTime(2020, 11, 10, 14, 54, 8, 213, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "8bb8fc7d-e0f4-464e-97ea-abb144f0d710", "AQAAAAEAACcQAAAAEKRCzl+AQel91TfK9lipyjFt0eY2bAfyQJ+5x8jWVAEhGJZDjU54PbW6NUfGmKtTKA==", new DateTime(2020, 11, 10, 14, 54, 8, 220, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications",
                column: "TimeSheetId",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d6e4e40f-a690-4452-9b49-8666141562a1", "AQAAAAEAACcQAAAAEOFpH7npn12sdRRcXS7uGl/DWAK3fr1UaSD2lvwGZ4K17PlFY6w5P4YFKsX7+sgT6g==", new DateTime(2020, 11, 9, 21, 51, 28, 700, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "813ae601-7bb6-47ed-9185-022348fd36cc", "AQAAAAEAACcQAAAAEMJH+Ctu4sKgMYI+IawtjuufD2bZwdtLB/1i9iW8ExlXzTG+7aj6iUx/r46VgZvoAg==", new DateTime(2020, 11, 9, 21, 51, 28, 709, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "b2991de0-e963-49ee-82f3-579a2304d4fe", "AQAAAAEAACcQAAAAEOYbYFgF6wJb1Hl7WW+BadYIHptX9A4C10+wssQSJTnFjkuTI4hQvrp0YeKFVTrTRA==", new DateTime(2020, 11, 9, 21, 51, 28, 733, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "14a7c712-b6bb-4872-85fe-5875e8bbea65", "AQAAAAEAACcQAAAAEDhkEqGJKYf0rentbrGdSdZ7Ad2m5q0dq+BVdHqART9U4G0sIq+dUFpPSofxEbzuHg==", new DateTime(2020, 11, 9, 21, 51, 28, 739, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "624783e4-3b1a-47dc-a7bf-9c9fc259dd31", "AQAAAAEAACcQAAAAEE6ZZ01eEMKMB+1ANwLLpiRKfm6V3j8CNaipa4nZsaQ/jkElt31z69s6ns2B9IQI0Q==", new DateTime(2020, 11, 9, 21, 51, 28, 746, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f565deab-15a6-4e43-bbc8-8867f7247dd2", "AQAAAAEAACcQAAAAEBZbZ9M4rz1ZrQF2+jRa1IDVLGU6wZqEWVT4R4j0IBQTWTooj84sqhZgHXN1vyVcSg==", new DateTime(2020, 11, 9, 21, 51, 28, 754, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "829903c6-c5f9-44cb-89dd-fb4d2a3981da", "AQAAAAEAACcQAAAAEDNwwj41JVX27812u3JATD9WhOjUVOC8rzF9//ugnh7XGo5fjqB5Avf1Y5bIkVcwvg==", new DateTime(2020, 11, 9, 21, 51, 28, 718, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "304e28c0-45c2-4924-a1c9-6d4acd45f645", "AQAAAAEAACcQAAAAELuhsRmioVKSYRJBqg6eZbkA3p3jhGPfDlGEw4djcebzxOXKebd4+Qi/QnxWWjjp9A==", new DateTime(2020, 11, 9, 21, 51, 28, 724, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_TimeSheets_TimeSheetId",
                table: "UserNotifications",
                column: "TimeSheetId",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
