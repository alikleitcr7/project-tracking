using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class notificationFlagNonNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NotificationFlag",
                table: "Users",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7431dc0e-b9e5-48bf-9819-29166970b27b", false, "AQAAAAEAACcQAAAAENrIDc83Io9N2FTdXQ2+nyYJz6k0fo7PRYrm3vNYpdOcnIUOPLulHgq0b4nKjRItJw==", new DateTime(2020, 11, 15, 13, 1, 32, 665, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "54883f37-b912-4b33-a356-b5ee6c844037", false, "AQAAAAEAACcQAAAAEN7NFF3yrzypLZs1cjFoQSzDOBsPU4GyJSfF9b73s1npAUSE++89oMEeUfAvwGqGWw==", new DateTime(2020, 11, 15, 13, 1, 32, 680, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "bec17caf-f066-46cd-8282-eed46b2adad1", false, "AQAAAAEAACcQAAAAEKzTd5IECylaCz7mKHRNf2WSBfH12b2L/D/pJ74cil2S/ZoPDYwoPj8NhW/f3ViBbg==", new DateTime(2020, 11, 15, 13, 1, 32, 716, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f3136df6-b575-4308-b168-cf7a94b9eedd", false, "AQAAAAEAACcQAAAAEOfgtmwyXT6MLO9vlt+dul9Jp1Vh4ALLSf/2EuKuFHFYKVcMCsRhy5Iz/CE7SmRFlA==", new DateTime(2020, 11, 15, 13, 1, 32, 728, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "10987b52-2677-4ef9-b959-e9c3a83df16c", false, "AQAAAAEAACcQAAAAEOtOwEBB4rzH8hZClVVwqBZjNDvrPnnjc/MhfcMLldwSL69waZ5H0Wyr6EHUyt1Evg==", new DateTime(2020, 11, 15, 13, 1, 32, 740, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "862f24ef-3423-4617-9fd8-17fdef0acc31", false, "AQAAAAEAACcQAAAAEJr12grG2LDxByai8LO4gspythu7ksLp+6CPDUQn+8SyGZwnfdxG+OnHp2S8qNLXYA==", new DateTime(2020, 11, 15, 13, 1, 32, 752, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1c167805-0766-4535-a5b3-7874af0d0d02", false, "AQAAAAEAACcQAAAAEFeQxCzQJ0YDDxGWMEZ2Xb2wfY/QXFy8zxFOuWyIT2wSdpu/P3vGGI8Mx96vo3UZPw==", new DateTime(2020, 11, 15, 13, 1, 32, 693, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1fc3d570-9339-4244-bdbe-b4b57729ace8", false, "AQAAAAEAACcQAAAAEGJnxTw+VWkiw5MhdNKi5dNJgPiMpBOP5CZazW7HHW578NzXdJmxUH84BH+Fr6MIzA==", new DateTime(2020, 11, 15, 13, 1, 32, 704, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NotificationFlag",
                table: "Users",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "a2e24925-d954-4db8-904e-5d8662050054", null, "AQAAAAEAACcQAAAAEFTWuD68++C/torhf6ze8M3bEXOiogMwELyySABn8JJQrAg5pA9D3oJtYLVjxp8Uew==", new DateTime(2020, 11, 15, 11, 58, 24, 625, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f77a5626-d561-4a57-8616-0bf5d7fd591e", null, "AQAAAAEAACcQAAAAEP1owN3GKpwPags4fvJzk6ea99Pf6+MFgCW1+YY7V2BCIRYZhVtO2Jq/D6K65HZVgA==", new DateTime(2020, 11, 15, 11, 58, 24, 635, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "39a01d63-3744-4f08-9c6f-9045bd2ed9bd", null, "AQAAAAEAACcQAAAAEAs1K5Z81oUG1Zav/BNxavKcbQm/192cLo3gVxR0u19DxqWhb5S815da9JfCPCay3w==", new DateTime(2020, 11, 15, 11, 58, 24, 665, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f302bf2b-f874-4712-a2a8-7287a4071160", null, "AQAAAAEAACcQAAAAEJD1TjW5SES6agekHd0G3uUOOzNl/4CpKUDohje8vVWHGiG2wiIwinjE8wNs+jErlw==", new DateTime(2020, 11, 15, 11, 58, 24, 672, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "844845da-4c75-4d8a-846f-091c44f6fdc7", null, "AQAAAAEAACcQAAAAEMgvHrDFkK6oIEZWkDxRt7YwZa+TMhYboScI5drP3kHPfBelzZzoHtPy+AzVeGe0gA==", new DateTime(2020, 11, 15, 11, 58, 24, 687, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c0492e72-306d-4d2e-ae42-338698529f75", null, "AQAAAAEAACcQAAAAEJRx7Rnbon11p7iXB17RWEp4C69lzoK6706kuA/j60mi2U/qWVYXHGUiGYC4nDcY8Q==", new DateTime(2020, 11, 15, 11, 58, 24, 695, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ec55bcf1-f960-4fe5-b379-e10bf539518f", null, "AQAAAAEAACcQAAAAEPUFR3axwj507FH0QFHSlaOli/bcbvGHob1unrSsMyciy5T9GZXIxkJXD134wP5gFw==", new DateTime(2020, 11, 15, 11, 58, 24, 645, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "NotificationFlag", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6889990e-9f10-4c86-a7c9-0f0c5f2bd81b", null, "AQAAAAEAACcQAAAAEA79utueanhV0DXgXTuH05xEGKxrsESEKrvYeSKyruShgjkWRxGxEjFQMGPWH5bqmA==", new DateTime(2020, 11, 15, 11, 58, 24, 657, DateTimeKind.Local) });
        }
    }
}
