using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class titleforseededusers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "a0848a39-7d09-42c6-b4fd-8652c34c964f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "b1b23327-c10e-41cb-9443-9af0405762b7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "FirstName", "PasswordHash" },
                values: new object[] { "75700a7b-bb54-4710-8383-2d08713389d9", "Admin", "AQAAAAEAACcQAAAAEOxKIGrtvUp/Rjnxq0s3inwzY0v/LFUeT61EC4NgEgJzosjgsw8l44kjYwceKmrFag==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "9895eb1f-be10-4455-88de-0bbf1b5d6e15", "Ali", "Kleit", "AQAAAAEAACcQAAAAECLyNragqNZelfaJtS+0iSGKM5WQ8GHmgOh7cOWexS0wxlmNlIzzvCH90PaXijiRAQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "c13a13c7-2c81-45a5-82dc-35e444d0cb2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "6a1089a5-634a-4ece-8d14-96a812ba99e5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "FirstName", "PasswordHash" },
                values: new object[] { "bc5ea225-8cb9-4ecf-8081-94242594ddfd", null, "AQAAAAEAACcQAAAAEKMzlZt6hyvg6yPjHOdcIW1Axansk84w9JdiCAqsYV5Gtjyh95eGNHdOdXKFaejRnQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "a1578d3b-7169-4d53-ba43-838323b61555", null, null, "AQAAAAEAACcQAAAAEFkk0ETelfbQxZP2kVdDNVQ7DDHH74A9o1Qm+OPQ7YeqMIF/YwHvp9DNBiMBy5P+nw==" });
        }
    }
}
