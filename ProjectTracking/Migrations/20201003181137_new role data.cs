using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class newroledata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e572", "f35cd48b-dd7e-4851-92e0-abb9ecf4d869", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e590", "dc248962-55e5-454d-b322-9b0f4b09ae5b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "EmploymentTypeCode", "FirstName", "HourlyRate", "IsTracked", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "MonthlySalary", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TeamId", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "0fd75d23-f03f-48da-860a-d69313ff7c07", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@sys.com", true, null, null, null, null, null, false, null, null, null, "ADMIN@SYS.COM", "ADMIN", "AQAAAAEAACcQAAAAEFN5CFUtPoLfkXv054JIcaxokZi/0YBJ3/5iVYRpHVS14OcwH0ysUvJ5G0cJ1gTB0A==", null, false, "a18be9c0-aa65-4af8-bd17-00bd9344e572", "", null, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "EmploymentTypeCode", "FirstName", "HourlyRate", "IsTracked", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "MonthlySalary", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TeamId", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e576", 0, "8b581366-d0fb-449e-8dfb-847b191e0b01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alikleitcr7@gmail.com", true, null, null, null, null, null, false, null, null, null, "ALIKLEITCR7@GMAIL.COM", "ALIKLEIT", "AQAAAAEAACcQAAAAEFqbO6uqs/300sedhGKD5ZOrRvVTcYQC7xxEfgF+YgGoNIkBAF+yFt9uEwsg6yLKCw==", null, false, "a18be9c0-aa65-4af8-bd17-00bd9344e590", "", null, null, false, "alikleit" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "0fd75d23-f03f-48da-860a-d69313ff7c07" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e576", "8b581366-d0fb-449e-8dfb-847b191e0b01" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e572", "f35cd48b-dd7e-4851-92e0-abb9ecf4d869" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e590", "dc248962-55e5-454d-b322-9b0f4b09ae5b" });
        }
    }
}
