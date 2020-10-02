using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class broadcasttouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_Teams_TeamID",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_Supervisers_ToTeamId_FromSupervisorId",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcasts_TeamID",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcasts_ToTeamId_FromSupervisorId",
                table: "Broadcasts");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "Broadcasts");

            migrationBuilder.DropColumn(
                name: "HoursPerDay",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FromSupervisorId",
                table: "Broadcasts",
                newName: "FromUserId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "d603061c-73f7-446d-9c0d-2475210ec106");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "e6d9f923-fc0a-4a7c-bbdc-45ee39a9727f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7da13084-cdea-4d27-a061-596694c15e7c", "AQAAAAEAACcQAAAAECC+7COLbJ5gmzNpP/6rJBsf+2QT5TbRZ/P3cBzfk7EvlYWwBFgB2AQSf+nMxx4HxQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_FromUserId",
                table: "Broadcasts",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_ToTeamId",
                table: "Broadcasts",
                column: "ToTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_AspNetUsers_FromUserId",
                table: "Broadcasts",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Teams_ToTeamId",
                table: "Broadcasts",
                column: "ToTeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_AspNetUsers_FromUserId",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_Teams_ToTeamId",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcasts_FromUserId",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcasts_ToTeamId",
                table: "Broadcasts");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "Broadcasts",
                newName: "FromSupervisorId");

            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "Broadcasts",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HoursPerDay",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "3cf0549e-d8dd-4bf5-ab5c-09c2f8acdc91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "4218e8ae-fbb7-4b5c-a312-a3b97d60d535");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7e12632-3570-4d60-a604-599834f2c896", "AQAAAAEAACcQAAAAENatCrwBB+9AZEXa+A8lyvZbEBJWPcGdwC9kMcAFzrjXrtB+Wf//3xGceOR0jOoHMw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_TeamID",
                table: "Broadcasts",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_ToTeamId_FromSupervisorId",
                table: "Broadcasts",
                columns: new[] { "ToTeamId", "FromSupervisorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Teams_TeamID",
                table: "Broadcasts",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Supervisers_ToTeamId_FromSupervisorId",
                table: "Broadcasts",
                columns: new[] { "ToTeamId", "FromSupervisorId" },
                principalTable: "Supervisers",
                principalColumns: new[] { "TeamId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
