using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class supervisorlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Users_AssignedByUserId",
                table: "Supervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Teams_TeamId",
                table: "Supervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Users_UserId",
                table: "Supervisor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supervisor",
                table: "Supervisor");

            migrationBuilder.RenameTable(
                name: "Supervisor",
                newName: "SupervisorLog");

            migrationBuilder.RenameIndex(
                name: "IX_Supervisor_UserId",
                table: "SupervisorLog",
                newName: "IX_SupervisorLog_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Supervisor_TeamId",
                table: "SupervisorLog",
                newName: "IX_SupervisorLog_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Supervisor_AssignedByUserId",
                table: "SupervisorLog",
                newName: "IX_SupervisorLog_AssignedByUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SupervisorLog",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "SupervisorLog",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupervisorLog",
                table: "SupervisorLog",
                column: "ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorLog_Users_AssignedByUserId",
                table: "SupervisorLog",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorLog_Teams_TeamId",
                table: "SupervisorLog",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorLog_Users_UserId",
                table: "SupervisorLog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorLog_Users_AssignedByUserId",
                table: "SupervisorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorLog_Teams_TeamId",
                table: "SupervisorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorLog_Users_UserId",
                table: "SupervisorLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupervisorLog",
                table: "SupervisorLog");

            migrationBuilder.RenameTable(
                name: "SupervisorLog",
                newName: "Supervisor");

            migrationBuilder.RenameIndex(
                name: "IX_SupervisorLog_UserId",
                table: "Supervisor",
                newName: "IX_Supervisor_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SupervisorLog_TeamId",
                table: "Supervisor",
                newName: "IX_Supervisor_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_SupervisorLog_AssignedByUserId",
                table: "Supervisor",
                newName: "IX_Supervisor_AssignedByUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Supervisor",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "Supervisor",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supervisor",
                table: "Supervisor",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "56202253-05b7-45a6-85e8-229553cc57d2", "AQAAAAEAACcQAAAAEJ57xGTs3bLcxdIpfKjdbLOyjXuC1+w3iqCyaP7nrJB0tNKA3blP4eA8HWvNYQn+4w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5561f264-4f2a-4ddb-bb83-8e40d2e44ec9", "AQAAAAEAACcQAAAAEJFNEpJNsq59rxTMcYVbAXyzs5/GC9qSAA8PS5dAhq0MNwosKJoQpge09UXyTcQ+5g==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Users_AssignedByUserId",
                table: "Supervisor",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Teams_TeamId",
                table: "Supervisor",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Users_UserId",
                table: "Supervisor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
