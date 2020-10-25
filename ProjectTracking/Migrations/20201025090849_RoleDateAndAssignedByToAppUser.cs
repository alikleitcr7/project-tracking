using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class RoleDateAndAssignedByToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleAssignedByUserId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RoleAssignedDate",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4587813-c783-491f-b7a1-9d80a5f195d1", "AQAAAAEAACcQAAAAEPGG2beJb5YXcDMtdz5bq1AgBMeXArMbBcNrsEDgcY0jfd8lAUmynmfe3cWhEnwNZA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aeb7af1f-849c-4ceb-ac57-85bc8ecac090", "AQAAAAEAACcQAAAAEA6QWvDWtnkQLRMzroKY48wtHjcL8xmLW43XUFdGPNyBEZFhs8+W4dwJB0f6gFAKJw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "23840ecc-0c24-4fdf-bf85-9fa324125b5c", "AQAAAAEAACcQAAAAEOpfJi10uhjCTpDNVYQK4XpP9W39sMrmC8F6dMrIcDsz2yVU/4LOtMC7jIYA51eaIQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "403ce82b-5959-4816-bd29-8535516e2ae0", "AQAAAAEAACcQAAAAEMx8kcUdRXaQxaVDks3VT5ymPEThEhFiCFWuBWMzjfVbFbI4sLbAzmVzbmdcEdN03Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9506ae3-a75c-483e-9a14-e1cc82e51377", "AQAAAAEAACcQAAAAEBJYey2ZzbDg9WSwXxauJIWtqAVzwVhNgRVRfICwD6l8vWtT6lnykBWnlVJayjpuFw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c393546-01d9-4886-93bf-cd16ec439c7f", "AQAAAAEAACcQAAAAEHLF+AsJz6DSh9gvJiiV6fSUY4J2Gd1m/KxafL6H1Gum9XaRivPaVVu8l+vjYmnhUA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "60bb0dc3-aee0-476d-815d-5360826dce81", "AQAAAAEAACcQAAAAELmVJ/is4O3xepdr8VTT6qpEesIDfTAAi4ERlbwIZVpPRwW+YUQy2wFjJYRvrCPzSw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90b57d5e-c9aa-4dac-8e33-ebc175a1fa02", "AQAAAAEAACcQAAAAEEJTDJAS6iWrxtWE3MPJVqiUrWXZpsbIKtfqT42LPWBcsJYcEP3dG0PckHxwUQoU8A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleAssignedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleAssignedDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "805e4bf9-073c-4a79-b1a5-82644051b85d", "AQAAAAEAACcQAAAAEEXdx7MrrhTXXLU65IpTCyJq7REN7TkzE7F2xx2nkQWqFx9mLPcnWQX/EdvAk4aFLg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7cb69094-49ea-4cb7-b5d7-6b8f89808adb", "AQAAAAEAACcQAAAAEOQjApP6JUfHAfsl7lQB4eNG0zu1KHNSwtjVz4VcU+B72pX/vjv9hOykvFhn1h/mcA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a493d95e-51d3-452a-a42e-61ba69c04008", "AQAAAAEAACcQAAAAELnq4l6+YpGup8qRVIbSqRcpOKNqUtRutHep5CXRsdSzH+yrXXjyv0RNVyNc7vC4xg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "574e07f1-642d-40fd-9a04-b0ef7c49c839", "AQAAAAEAACcQAAAAEMIwNWVYlQO/mYy2LCBqJJWcFIlUqng8UL4kvOFkk/4WvteyAJ7EKisXXiAyuaaBEA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fb09e74-5034-42b6-8cb0-387860d31fb7", "AQAAAAEAACcQAAAAEGghRZk3O4LL/fQwhhBEwk3c5//H+rXpCd4jssGpegMAhD6c291RRMoYqZiIcQVqRw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fed72d0-d535-481a-a075-0e86eb3097e9", "AQAAAAEAACcQAAAAEOwxmMKWC/LJSfbpGXC4fbx9x2pLPXC3+ybzqE+Fb29r1IyitWiGZMbEyijasVFSHQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71843d2c-94c8-4ffd-8c4f-5076b2a8951c", "AQAAAAEAACcQAAAAEAA7Q3HRGI98Su9+nd+58GE9nkhWkWQa0A4OcLBADVafeZUlFXi478CURid1sGzCIA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7cff19a-a3b8-4e9e-a9ad-4ae940188e6c", "AQAAAAEAACcQAAAAEMEz3lPsu9fBMOEewJIHEtccOZGcGYkGR7WWCYwRWiH8265AUrfl037DAOxhXzuhfg==" });
        }
    }
}
