using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class user_refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgreementType",
                table: "AspNetUsers",
                newName: "EmploymentTypeCode");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "73d52fbc-f1d8-46cd-8bca-00dc7fffe8ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "b7dbe09d-89de-44e7-af3e-c7c5e00efef2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "176b607f-a2ed-44e8-874d-668fa7338b5c", "AQAAAAEAACcQAAAAEAdMFndRZ2P+jsQvsUc/kLwL38hKgyNfDRkUUJA08quNFNx4FKHwwoiOtBdN+u1neg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmploymentTypeCode",
                table: "AspNetUsers",
                newName: "AgreementType");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "0fada57e-1a75-4c38-a326-95589c9b8d09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "535f4c2f-6137-4738-a6f0-ba153ba66962");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "beb81415-94d1-4c59-927c-cf2fae0c469b", "AQAAAAEAACcQAAAAECBcnkn02lj2cFXUE8q3RWi54pDugOxEkaWkNYnUTMZ+4wURSju0PEc6X9EY/q28HA==" });
        }
    }
}
