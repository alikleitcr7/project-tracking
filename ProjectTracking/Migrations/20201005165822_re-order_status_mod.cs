using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class reorder_status_mod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c038fc8e-3043-4797-82db-ce8ba3542c75", "AQAAAAEAACcQAAAAELKFOi3kQLerM47v90J4hn66CGNLXrJgFrWa/ZTCQ1Zi1ocgMD47hwf1XfW24M/qpw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7fb0b1f4-a623-42ce-973b-8ecbc40917d0", "AQAAAAEAACcQAAAAEOA+icTE9sog77SmDC5yhomE4DHn7iT0+bVKn8ST/ljaWF2i8kZYzxhCZLWGbZ2LYQ==" });
        }
    }
}
