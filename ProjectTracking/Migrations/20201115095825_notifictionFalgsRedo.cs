using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class notifictionFalgsRedo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "UserNotifications",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Broadcasts",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "a2e24925-d954-4db8-904e-5d8662050054", "AQAAAAEAACcQAAAAEFTWuD68++C/torhf6ze8M3bEXOiogMwELyySABn8JJQrAg5pA9D3oJtYLVjxp8Uew==", new DateTime(2020, 11, 15, 11, 58, 24, 625, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f77a5626-d561-4a57-8616-0bf5d7fd591e", "AQAAAAEAACcQAAAAEP1owN3GKpwPags4fvJzk6ea99Pf6+MFgCW1+YY7V2BCIRYZhVtO2Jq/D6K65HZVgA==", new DateTime(2020, 11, 15, 11, 58, 24, 635, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "39a01d63-3744-4f08-9c6f-9045bd2ed9bd", "AQAAAAEAACcQAAAAEAs1K5Z81oUG1Zav/BNxavKcbQm/192cLo3gVxR0u19DxqWhb5S815da9JfCPCay3w==", new DateTime(2020, 11, 15, 11, 58, 24, 665, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f302bf2b-f874-4712-a2a8-7287a4071160", "AQAAAAEAACcQAAAAEJD1TjW5SES6agekHd0G3uUOOzNl/4CpKUDohje8vVWHGiG2wiIwinjE8wNs+jErlw==", new DateTime(2020, 11, 15, 11, 58, 24, 672, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "844845da-4c75-4d8a-846f-091c44f6fdc7", "AQAAAAEAACcQAAAAEMgvHrDFkK6oIEZWkDxRt7YwZa+TMhYboScI5drP3kHPfBelzZzoHtPy+AzVeGe0gA==", new DateTime(2020, 11, 15, 11, 58, 24, 687, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c0492e72-306d-4d2e-ae42-338698529f75", "AQAAAAEAACcQAAAAEJRx7Rnbon11p7iXB17RWEp4C69lzoK6706kuA/j60mi2U/qWVYXHGUiGYC4nDcY8Q==", new DateTime(2020, 11, 15, 11, 58, 24, 695, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ec55bcf1-f960-4fe5-b379-e10bf539518f", "AQAAAAEAACcQAAAAEPUFR3axwj507FH0QFHSlaOli/bcbvGHob1unrSsMyciy5T9GZXIxkJXD134wP5gFw==", new DateTime(2020, 11, 15, 11, 58, 24, 645, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6889990e-9f10-4c86-a7c9-0f0c5f2bd81b", "AQAAAAEAACcQAAAAEA79utueanhV0DXgXTuH05xEGKxrsESEKrvYeSKyruShgjkWRxGxEjFQMGPWH5bqmA==", new DateTime(2020, 11, 15, 11, 58, 24, 657, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "UserNotifications",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Broadcasts",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7b9ae2a4-a1e7-41d7-a233-a0e9cdbc6a43", "AQAAAAEAACcQAAAAEF7Sf5Outd+2/hoSdx67r3VrA37g9GCfCE/Yq3rYdoerricqZifu9E4adz9FFc70BA==", new DateTime(2020, 11, 15, 11, 56, 30, 606, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "3ee5cfb0-58a7-4cf3-b908-964711978153", "AQAAAAEAACcQAAAAEDI9EH6mQaMzi9+cBEwvtHVFY7Z0hoabUsfVedOd24EZdoFG642tmjY+GsTc3n6WiA==", new DateTime(2020, 11, 15, 11, 56, 30, 615, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "49846b78-9199-46c2-8d18-be9e4c712890", "AQAAAAEAACcQAAAAEE3bU7r3lafUCWal7Pp9et4o1su/arcSsJUeqBX4tNU86oRT44288cGf3HXlN5X8Pw==", new DateTime(2020, 11, 15, 11, 56, 30, 636, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c34c3d0d-b4e5-4050-8631-1c77ce719a3a", "AQAAAAEAACcQAAAAEDXOuSIJQh/7ih0lXEPuVOZKedgLSsHvCf8bNnvhrx1NzGVp/p8xyxQjxrRsU5jZEw==", new DateTime(2020, 11, 15, 11, 56, 30, 643, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "df6cecc8-36a1-449d-a392-ea282d7942ae", "AQAAAAEAACcQAAAAEL3bQ2q+Ur0+slcL6EPjHP3plOzZ3vUrtLQSGPqJ/SNTBCPu6YXgBePlTku3MYMNZQ==", new DateTime(2020, 11, 15, 11, 56, 30, 649, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5f82c0bc-6609-4dc2-b615-bf9499903c0d", "AQAAAAEAACcQAAAAEEtFBqnBNH5cNB1AVOQZBzYW8StPYkdLmv90ZiENOyH6AJz6sgmZjmKwqZbODoHXDw==", new DateTime(2020, 11, 15, 11, 56, 30, 656, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "43c202d3-bc45-421f-8730-54e2bfa72b41", "AQAAAAEAACcQAAAAEF0565JHye8SHrnHBLOcrxyz1Cj5sx+LUT7ZCsyayzMqWFRMehqv6i27un4B4Tg1Ug==", new DateTime(2020, 11, 15, 11, 56, 30, 622, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fb796dd8-d0e2-46eb-ad1a-716627e65175", "AQAAAAEAACcQAAAAEBeC/k5s0z2wEoTtkZiadPTauCS/IFYOEyL+IVYAf5NgeXXi2itjBpaptkX/3fQWZA==", new DateTime(2020, 11, 15, 11, 56, 30, 629, DateTimeKind.Local) });
        }
    }
}
