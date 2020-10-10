using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usertitlenotreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Users",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Users",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

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
    }
}
