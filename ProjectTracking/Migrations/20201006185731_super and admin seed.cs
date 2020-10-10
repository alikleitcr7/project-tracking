using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class superandadminseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Users",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "TimeSheets",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AssignedByUserId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAssigned",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SupervisorId",
                table: "Teams",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "56202253-05b7-45a6-85e8-229553cc57d2", "Sys", "Admin", "AQAAAAEAACcQAAAAEJ57xGTs3bLcxdIpfKjdbLOyjXuC1+w3iqCyaP7nrJB0tNKA3blP4eA8HWvNYQn+4w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5561f264-4f2a-4ddb-bb83-8e40d2e44ec9", "AQAAAAEAACcQAAAAEJFNEpJNsq59rxTMcYVbAXyzs5/GC9qSAA8PS5dAhq0MNwosKJoQpge09UXyTcQ+5g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedByUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DateAssigned",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Users",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "TimeSheets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "91bd800c-1d8b-46f4-a140-b522278a3c20", "Admin", null, "AQAAAAEAACcQAAAAEKA3D3AZP+whfnvGIzVaa7lVh9XNLC/67d81ui1TE4yg43USazce4OPm8a3RLSWfkA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "153446af-835d-4f69-b390-f61c92cef8f5", "AQAAAAEAACcQAAAAEHGh8IH2txPe3Z9OPiQw+3wFInoIJ4EppLA5jNsSBmX5QTpFyvwH6vTeYcKE2ipYkg==" });
        }
    }
}
