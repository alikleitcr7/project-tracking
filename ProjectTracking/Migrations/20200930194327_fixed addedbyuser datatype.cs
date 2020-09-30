using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class fixedaddedbyuserdatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AddedByUserId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AddedByUserId1",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "0879f308-68cf-4468-9f27-377f1041a3fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "a2f2109e-1876-4f94-8479-18079419007c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e36e4f8c-b49d-4ddf-a2ca-ffaf20532a76", "AQAAAAEAACcQAAAAEKt6BZ5goYy5Ww9XGcsIGjgHAJkkmaA3wJiadxNqDzeObXxvh5b9IUbEI+Y7lL8NuA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedByUserId",
                table: "Projects",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId",
                table: "Projects",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AddedByUserId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "AddedByUserId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId1",
                table: "Projects",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "9dbd257d-373d-45b7-aac2-3064301aa100");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "d24e9816-a990-4abc-b57a-233e84481de7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7f225ef-a9c2-4ced-9885-cf546d9290aa", "AQAAAAEAACcQAAAAEMW1wEhmLewdayM4Ihxuf1Uf9hVeo92lB0ZaSrFNL7QMyYI7YUV+IbkXHeb0v10a1g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedByUserId1",
                table: "Projects",
                column: "AddedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AddedByUserId1",
                table: "Projects",
                column: "AddedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
