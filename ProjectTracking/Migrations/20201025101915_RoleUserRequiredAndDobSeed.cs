using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class RoleUserRequiredAndDobSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RoleAssignedDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleAssignedByUserId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fa8f9049-5bfb-40e0-9f73-d3be00973dc9", new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEOuDwlC8h2RL2ypvg+sQMwKTaftXtUwdt6iqxI8SZsw7aoBQ2mqpeZpv8k5v2uyY/g==", new DateTime(2020, 10, 25, 12, 19, 14, 880, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2596ed32-692b-4a79-8422-489e423ef4ba", new DateTime(1996, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEH8VTVw6IEhsivxexvW8PH+zJqMkURd1E9e8vQ8tV+5w9iu3CkyQmqAjZ5cyeY9TwQ==", new DateTime(2020, 10, 25, 12, 19, 14, 891, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "c19ccc49-f799-4511-9ce6-aa09bc811530", "AQAAAAEAACcQAAAAEIfsfvSwFBPn7xvouT56jDcoH/chUeXMmEVlQmlWNoLgrz8NYTpLWjL8wkdLDtjE5Q==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 923, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "5e070fd3-2d94-4fd0-9d08-ca5a01227204", "AQAAAAEAACcQAAAAEC2PvQzCnzIDnx67hAw8KV2CS3Z13PaRkkPH2qJnCl85b3AlNFc0Gkpujq5vwhMtmw==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 936, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "a85ea73b-d631-48ff-90e3-440312a8fd6f", "AQAAAAEAACcQAAAAEHSZtDuQWgoU3s6te/1CXGzP53w6q6h861Ufpc/5lBMDhY6afAxRLVkEBXnS3ULzFg==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 950, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "7145be7a-3c79-4c68-b351-8670fe9484ea", "AQAAAAEAACcQAAAAECysQswZ+muoBU61vzkZEBKx9SF83Z52lf4lqZbf7hh0hVWPa05pbYBchkMWx9kaXg==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 964, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "50287cad-bbd5-42e1-8e12-aafa9633f899", "AQAAAAEAACcQAAAAEJ7SWjwqlWK8ALzJonFOUkdfsnA/GxcF+n9s2EHaYc6jEjQOCocsj+r6Ea3WlM77aw==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 900, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "2bdca60b-7952-4726-b6b5-d398dada84da", "AQAAAAEAACcQAAAAED14m0fQxXc6+QRKMwEeGQtNGaXB1enTG6RS+EYzhtgw6QwdzvigB4daZLNsibLXWA==", "a18be9c0-aa65-4af8-bd17-00bd9344e575", new DateTime(2020, 10, 25, 12, 19, 14, 909, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleAssignedByUserId",
                table: "Users",
                column: "RoleAssignedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RoleAssignedByUserId",
                table: "Users",
                column: "RoleAssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RoleAssignedByUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleAssignedByUserId",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RoleAssignedDate",
                table: "Users",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "RoleAssignedByUserId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c4587813-c783-491f-b7a1-9d80a5f195d1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEPGG2beJb5YXcDMtdz5bq1AgBMeXArMbBcNrsEDgcY0jfd8lAUmynmfe3cWhEnwNZA==", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "aeb7af1f-849c-4ceb-ac57-85bc8ecac090", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEA6QWvDWtnkQLRMzroKY48wtHjcL8xmLW43XUFdGPNyBEZFhs8+W4dwJB0f6gFAKJw==", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "23840ecc-0c24-4fdf-bf85-9fa324125b5c", "AQAAAAEAACcQAAAAEOpfJi10uhjCTpDNVYQK4XpP9W39sMrmC8F6dMrIcDsz2yVU/4LOtMC7jIYA51eaIQ==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "403ce82b-5959-4816-bd29-8535516e2ae0", "AQAAAAEAACcQAAAAEMx8kcUdRXaQxaVDks3VT5ymPEThEhFiCFWuBWMzjfVbFbI4sLbAzmVzbmdcEdN03Q==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "b9506ae3-a75c-483e-9a14-e1cc82e51377", "AQAAAAEAACcQAAAAEBJYey2ZzbDg9WSwXxauJIWtqAVzwVhNgRVRfICwD6l8vWtT6lnykBWnlVJayjpuFw==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "5c393546-01d9-4886-93bf-cd16ec439c7f", "AQAAAAEAACcQAAAAEHLF+AsJz6DSh9gvJiiV6fSUY4J2Gd1m/KxafL6H1Gum9XaRivPaVVu8l+vjYmnhUA==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "60bb0dc3-aee0-476d-815d-5360826dce81", "AQAAAAEAACcQAAAAELmVJ/is4O3xepdr8VTT6qpEesIDfTAAi4ERlbwIZVpPRwW+YUQy2wFjJYRvrCPzSw==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedByUserId", "RoleAssignedDate" },
                values: new object[] { "90b57d5e-c9aa-4dac-8e33-ebc175a1fa02", "AQAAAAEAACcQAAAAEEJTDJAS6iWrxtWE3MPJVqiUrWXZpsbIKtfqT42LPWBcsJYcEP3dG0PckHxwUQoU8A==", null, null });
        }
    }
}
