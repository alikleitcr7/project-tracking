using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class tablernm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "937278cf-e704-425c-9b73-7f2d11e9344f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "466f9821-bbbd-4ce7-a12a-36a5b3459e3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "459d3ad3-04c0-4a6e-b0bd-526eb7154df8", "AQAAAAEAACcQAAAAEGNzvozZqtI32TYqN+yXoujfrIiQ+tRUM/zGuTLc2k+nmTSl39sBWgFlcSdCDLNRjA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "23b5642d-8bc8-4919-9456-9052d9d8947b", "AQAAAAEAACcQAAAAEPDJtgGMt/0djDdSBa58JKvqyyCeNkBXQh4ztYa0wHjXyilRJ27Mkh5oI5L9IMXoGw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "e68c2d08-ffdf-4607-a26c-e66c53d0c4e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "e60f8ea8-1fcb-46b6-93fe-f6bddb9aebc5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc49aebf-b080-4d00-b16c-1a5ae58b2b92", "AQAAAAEAACcQAAAAEDmupmIgochdZIuOK79PpMeC93ulTnpiOOaeJHGWVNRKAWOaMSwr1nYy+BH7zMh9uQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6c5aa906-3923-4804-a9c1-bde9ced06ff2", "AQAAAAEAACcQAAAAEMokMMD+q3hHsDDEmpy7JOfFBZhJomfzCax3vsin2bqLgX/zMjaCeEqAhCFvlzCX2g==" });
        }
    }
}
