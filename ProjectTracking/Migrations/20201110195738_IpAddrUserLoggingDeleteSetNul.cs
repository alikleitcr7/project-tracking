using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class IpAddrUserLoggingDeleteSetNul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "730b3e18-0381-4b30-9be0-a120500686d8", "AQAAAAEAACcQAAAAEOhyCW3XGoiIAFA3TZvIwlsPgt0D+PA8QY6s4ZHNTM195dVrqcQYLrUHjukt+EVK/w==", new DateTime(2020, 11, 10, 21, 57, 38, 365, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ce04ff31-99c6-4c43-9539-25ed60183ec6", "AQAAAAEAACcQAAAAEJSx+aohd+9JQOMwGriFa+2SskN2M5RJEM+8dPLNaxnf4UqN8ggBodTS6tqogbtU+w==", new DateTime(2020, 11, 10, 21, 57, 38, 373, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "46a59529-58f1-4462-96a1-677210e19aa7", "AQAAAAEAACcQAAAAEE6nzF6y/58bPo33uOeCLj0y3eDI7ulUWAQG5k1ZIhwKUGlX6G46be/E4Sl94pTJWA==", new DateTime(2020, 11, 10, 21, 57, 38, 399, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "3aeec919-0041-4ecf-811d-757c45ee3538", "AQAAAAEAACcQAAAAEC0n1L6Z7FgeoEZJl1LjRc2JbOaE83jMoH1u/Sm5Kh1dOzb5MwJ558BCK3YdFn4YSA==", new DateTime(2020, 11, 10, 21, 57, 38, 405, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d6b88e31-b343-458e-a2c6-69ba60bf7d49", "AQAAAAEAACcQAAAAEP/xfraHq5zd+BopqiJr1r5xBzw3U0wWIPXgXCzUrATiqS0humcyLla3rwTjdDCt4w==", new DateTime(2020, 11, 10, 21, 57, 38, 412, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d7f3d125-8e40-479c-8163-3217417a8631", "AQAAAAEAACcQAAAAEJCZOq+od/d6hTwG90S3FMssEzRldwNR1y6WXSkVAL2fCbA+oDcrWcRPU/OWxEsYFg==", new DateTime(2020, 11, 10, 21, 57, 38, 419, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "89e7fefb-bd0e-487a-8a60-d31f2494d085", "AQAAAAEAACcQAAAAEJuA6rKt4ZeDG0R5SrJf/UJQIIaUsQ6+fI2U1VWy5Y1H43k8TpbK2WalmjpdVSrSyg==", new DateTime(2020, 11, 10, 21, 57, 38, 385, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "9dd65704-67ee-47ed-8c17-feda933e232f", "AQAAAAEAACcQAAAAEFiYDkDNO20JYSlhQFlsO+m/r9AJpgIroeo6A7kTqx6/D2q2zTYolgp2JjqeNBlmUw==", new DateTime(2020, 11, 10, 21, 57, 38, 392, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging",
                column: "IpAdd",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging");

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
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging",
                column: "IpAdd",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
