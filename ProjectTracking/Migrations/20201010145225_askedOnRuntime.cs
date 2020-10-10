using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class askedOnRuntime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71d575e1-bdac-4272-b275-3d9166ea215b", "AQAAAAEAACcQAAAAECbpQh6EONI65oxBc8SLyZ07Sey6jPIntirMSIrmNizfdElWuN9VHiZ0lz5T75IxXg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1665f2e5-87a0-4a89-96fb-9d8b5bf43b78", "AQAAAAEAACcQAAAAEDAhQ27yYi1Fz+UaJh5eemaNL9EBI/simSPTmOUq7Q/lgEKF+x+hVGjuCgWJcllfdA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba79376d-46f3-4171-86c6-f496f8813074", "AQAAAAEAACcQAAAAEO9/XUlzUFAsxIgsFg72iZwaRu4pMPnBNwLEpusRcczZy/fFhyo+F0htvR8c7/XS+g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2c079c2c-3d91-454c-b663-a4900589fc5b", "AQAAAAEAACcQAAAAEI6IKXhpjv5b2yqiLqL01FlgwrHT/CvTQyqkohyEURe4qUkgfMUcBhW2e8bdzj2B8w==" });
        }
    }
}
