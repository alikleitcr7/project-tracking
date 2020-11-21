using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class rolelog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "UserRoleLogs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "309df6c5-bb2c-40c3-bf8a-5b8b87ae4b48", "AQAAAAEAACcQAAAAEARMWuW3ei/MS45uKFbn4iemlWtfr9DdME9dVf1dhBilB2JtiJfYNz6BD6SYNg/MnQ==", new DateTime(2020, 11, 21, 22, 51, 1, 200, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "0c74c91a-3176-4c53-b54c-ebdea1e076c0", "AQAAAAEAACcQAAAAEIN6nGwSf8PEjbMrD8r/BtHmvtcMVPf58fc0sV44KQpx24d9jp+RNMUseIb32RttCQ==", new DateTime(2020, 11, 21, 22, 51, 1, 209, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "55adec1d-f66c-4b69-afd5-c9147dcd277c", "AQAAAAEAACcQAAAAELGHPH+s0HxZvlV5Ja4P5QxI7MLjwwJBFl9awBxQxgUdjE3JOTlixLUtWz+okSuP8Q==", new DateTime(2020, 11, 21, 22, 51, 1, 231, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "4ab4330e-2332-4243-ad11-a8bf66ba704d", "AQAAAAEAACcQAAAAEFu1drs3XNBFh5AeLLW5OpQ+GlFDyjmAQcr7CjonQL2U4a4rdhFvSh97s6wd8xq15g==", new DateTime(2020, 11, 21, 22, 51, 1, 238, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6fd614d0-b13c-49a8-88f6-bd578e65c8da", "AQAAAAEAACcQAAAAEIkm69XhWj8A1guxhiSXlx9rzjsw6Pud7F8KbM57qnYIu6pPEfeaKi+iT/8VWLzUGA==", new DateTime(2020, 11, 21, 22, 51, 1, 244, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "8f52425a-ea92-4a25-9dab-9591c4f772ad", "AQAAAAEAACcQAAAAEDYFO65Wd2mdWMru2yqpIaAr72PiAr/uoZVJw4w1iai1cAxH8iaF4jLPRQIoGLZVIA==", new DateTime(2020, 11, 21, 22, 51, 1, 251, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "052df201-9bfd-47e5-b485-349e89fa2740", "AQAAAAEAACcQAAAAEMkspJ+ZgHeeiHObX7ejE/FoWOG/zyhK8+wvo+dXnwXiOX64dlAcOZAnxzO/BIsylQ==", new DateTime(2020, 11, 21, 22, 51, 1, 217, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "9b5a1a87-829f-46e9-98ad-2c6566ccfdef", "AQAAAAEAACcQAAAAEBDP1nl+LoMxMcpYFRjpRvPWt9mXMW0qd9KEerF1Ldhqz8yTj7NVpuLIQ+X6396KSQ==", new DateTime(2020, 11, 21, 22, 51, 1, 224, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "UserRoleLogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7431dc0e-b9e5-48bf-9819-29166970b27b", "AQAAAAEAACcQAAAAENrIDc83Io9N2FTdXQ2+nyYJz6k0fo7PRYrm3vNYpdOcnIUOPLulHgq0b4nKjRItJw==", new DateTime(2020, 11, 15, 13, 1, 32, 665, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "54883f37-b912-4b33-a356-b5ee6c844037", "AQAAAAEAACcQAAAAEN7NFF3yrzypLZs1cjFoQSzDOBsPU4GyJSfF9b73s1npAUSE++89oMEeUfAvwGqGWw==", new DateTime(2020, 11, 15, 13, 1, 32, 680, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "bec17caf-f066-46cd-8282-eed46b2adad1", "AQAAAAEAACcQAAAAEKzTd5IECylaCz7mKHRNf2WSBfH12b2L/D/pJ74cil2S/ZoPDYwoPj8NhW/f3ViBbg==", new DateTime(2020, 11, 15, 13, 1, 32, 716, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f3136df6-b575-4308-b168-cf7a94b9eedd", "AQAAAAEAACcQAAAAEOfgtmwyXT6MLO9vlt+dul9Jp1Vh4ALLSf/2EuKuFHFYKVcMCsRhy5Iz/CE7SmRFlA==", new DateTime(2020, 11, 15, 13, 1, 32, 728, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "10987b52-2677-4ef9-b959-e9c3a83df16c", "AQAAAAEAACcQAAAAEOtOwEBB4rzH8hZClVVwqBZjNDvrPnnjc/MhfcMLldwSL69waZ5H0Wyr6EHUyt1Evg==", new DateTime(2020, 11, 15, 13, 1, 32, 740, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "862f24ef-3423-4617-9fd8-17fdef0acc31", "AQAAAAEAACcQAAAAEJr12grG2LDxByai8LO4gspythu7ksLp+6CPDUQn+8SyGZwnfdxG+OnHp2S8qNLXYA==", new DateTime(2020, 11, 15, 13, 1, 32, 752, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1c167805-0766-4535-a5b3-7874af0d0d02", "AQAAAAEAACcQAAAAEFeQxCzQJ0YDDxGWMEZ2Xb2wfY/QXFy8zxFOuWyIT2wSdpu/P3vGGI8Mx96vo3UZPw==", new DateTime(2020, 11, 15, 13, 1, 32, 693, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1fc3d570-9339-4244-bdbe-b4b57729ace8", "AQAAAAEAACcQAAAAEGJnxTw+VWkiw5MhdNKi5dNJgPiMpBOP5CZazW7HHW578NzXdJmxUH84BH+Fr6MIzA==", new DateTime(2020, 11, 15, 13, 1, 32, 704, DateTimeKind.Local) });
        }
    }
}
