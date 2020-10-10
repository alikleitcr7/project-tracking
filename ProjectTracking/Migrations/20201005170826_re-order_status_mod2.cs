using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class reorder_status_mod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca03ea89-c88c-489b-9f4a-ed1ac4ba79c4", "AQAAAAEAACcQAAAAEPar/ZotfPZiBbTehtwmmZy6qiZkX5zB9PYSWbgeXa1Vh9sbZigq7Eaz11YqgzSXaQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b4e15c5-28cb-4cee-b52b-7107298d4838", "AQAAAAEAACcQAAAAEAMBuQWlCkmP/88upcDV7NPCeQV8LI+ZVgoI30ewmT1KC62QGsv1lWkF+49d1bJ6bA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5522f3c-eb62-44a0-8f07-f7af0641ea20", "AQAAAAEAACcQAAAAEKJJ4S1UsSBSWDvH8EhUNO5QMZ4w4k2d3/D8XsDw4ErDAMsJWUQkOlHujS4P+rZ/hA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5033171e-ef13-4feb-beb6-b2596bc010a1", "AQAAAAEAACcQAAAAEDgdupPI8Az/mZ1iB0BH5Re2upXRJEs0k4Dy8QLozvItFEgE6yxXCVxQ6gYSdTIAgA==" });
        }
    }
}
