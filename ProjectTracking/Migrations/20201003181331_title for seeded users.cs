using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class titleforseededusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Title" },
                values: new object[] { "bc5ea225-8cb9-4ecf-8081-94242594ddfd", "AQAAAAEAACcQAAAAEKMzlZt6hyvg6yPjHOdcIW1Axansk84w9JdiCAqsYV5Gtjyh95eGNHdOdXKFaejRnQ==", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Title" },
                values: new object[] { "a1578d3b-7169-4d53-ba43-838323b61555", "AQAAAAEAACcQAAAAEFkk0ETelfbQxZP2kVdDNVQ7DDHH74A9o1Qm+OPQ7YeqMIF/YwHvp9DNBiMBy5P+nw==", "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "f35cd48b-dd7e-4851-92e0-abb9ecf4d869");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "dc248962-55e5-454d-b322-9b0f4b09ae5b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Title" },
                values: new object[] { "0fd75d23-f03f-48da-860a-d69313ff7c07", "AQAAAAEAACcQAAAAEFN5CFUtPoLfkXv054JIcaxokZi/0YBJ3/5iVYRpHVS14OcwH0ysUvJ5G0cJ1gTB0A==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Title" },
                values: new object[] { "8b581366-d0fb-449e-8dfb-847b191e0b01", "AQAAAAEAACcQAAAAEFqbO6uqs/300sedhGKD5ZOrRvVTcYQC7xxEfgF+YgGoNIkBAF+yFt9uEwsg6yLKCw==", null });
        }
    }
}
