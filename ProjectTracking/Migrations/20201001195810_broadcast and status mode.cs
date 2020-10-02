using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class broadcastandstatusmode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.CreateTable(
                name: "Broadcasts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    NotificationTypeCode = table.Column<short>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: false),
                    FromSupervisorId = table.Column<string>(nullable: true),
                    ToTeamId = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcasts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Broadcasts_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Broadcasts_Supervisers_ToTeamId_FromSupervisorId",
                        columns: x => new { x.ToTeamId, x.FromSupervisorId },
                        principalTable: "Supervisers",
                        principalColumns: new[] { "TeamId", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatusModification",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatusModification", x => new { x.ProjectId, x.DateModified });
                    table.ForeignKey(
                        name: "FK_ProjectStatusModification_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTaskStatusModification",
                columns: table => new
                {
                    ProjectTaskId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskStatusModification", x => new { x.ProjectTaskId, x.DateModified });
                    table.ForeignKey(
                        name: "FK_ProjectTaskStatusModification_ProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    NotificationTypeCode = table.Column<short>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: false),
                    FromUserId = table.Column<string>(nullable: true),
                    ToUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_FromUserId",
                table: "UserNotifications",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ToUserId",
                table: "UserNotifications",
                column: "ToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Broadcasts");

            migrationBuilder.DropTable(
                name: "ProjectStatusModification");

            migrationBuilder.DropTable(
                name: "ProjectTaskStatusModification");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateSent = table.Column<DateTime>(nullable: false),
                    FromUserId = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    NotificationTypeCode = table.Column<short>(nullable: false),
                    ToUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Notifications_FromUserId",
                table: "Notifications",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ToUserId",
                table: "Notifications",
                column: "ToUserId");
        }
    }
}
