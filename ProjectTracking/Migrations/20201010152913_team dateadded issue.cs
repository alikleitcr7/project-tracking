using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class teamdateaddedissue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f8f68f2-0c83-44dc-b493-844170ae3776", "AQAAAAEAACcQAAAAEAH2otD4wctid6jtU2HgqcXkRIDY6jyGwP0N5dwupaDhcZuiR+M1P8ndwYIOUiG72w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f10bba6-8ba2-4f65-999f-89a5cc3af249", "AQAAAAEAACcQAAAAELeP3rpHILU1HbgBgiLkN5ISX+FCSWTo6FDBhNn5eQciJ42w090lIYCKUwp+O0tp1A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1eff4cf6-f779-4999-842b-2df698a23baf", "AQAAAAEAACcQAAAAEHxldJSsIdmeQ4H0MHfJ1Y4QFZAZWNaE1LGnWm+zUci/UVq8AWtD63mRqMwO2dupmg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a406c49e-7a1e-46ab-bc5a-3a5dd78ca655", "AQAAAAEAACcQAAAAEGS6lcCqwIlUjO+EJ274mKjtCSV7VBX78werhNHsLzU49B0jBpWx+TF3X7hb2kXBfA==" });
        }
    }
}
