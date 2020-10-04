using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class removedapprolesds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "5ace2e68-42f6-415a-96d7-63a3841ce6cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "bf56f93f-2e99-434c-acea-4c49e96f3f80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f9274bcf-0c5c-49c9-9716-28ceaaa1ac26", "AQAAAAEAACcQAAAAEPQa7JuQA7+Pit4cZRGyQXNMWX46G42+pkm/xQWweoecyBmRc7Q2kQBa1XZFI8rSPA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "521768a0-7fc4-43b9-bcb4-b2a2f7865af6", "AQAAAAEAACcQAAAAEOdGumyVFl/gEp7beJRTkoHmLE6G3Lw7a/Gj/nbJkc9ZXMQO9h+pl9IBOoEKwxJ76w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "75700a7b-bb54-4710-8383-2d08713389d9", "AQAAAAEAACcQAAAAEOxKIGrtvUp/Rjnxq0s3inwzY0v/LFUeT61EC4NgEgJzosjgsw8l44kjYwceKmrFag==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9895eb1f-be10-4455-88de-0bbf1b5d6e15", "AQAAAAEAACcQAAAAECLyNragqNZelfaJtS+0iSGKM5WQ8GHmgOh7cOWexS0wxlmNlIzzvCH90PaXijiRAQ==" });
        }
    }
}
