using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IpAddresses",
                columns: table => new
                {
                    Address = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddresses", x => x.Address);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    StatusCode = table.Column<short>(nullable: false),
                    AddedByUserId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    PlannedEnd = table.Column<DateTime>(nullable: true),
                    ActualEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatusModifications",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatusModifications", x => new { x.ProjectId, x.DateModified });
                    table.ForeignKey(
                        name: "FK_ProjectStatusModifications_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    PlannedEnd = table.Column<DateTime>(nullable: true),
                    ActualEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTaskStatusModifications",
                columns: table => new
                {
                    ProjectTaskId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskStatusModifications", x => new { x.ProjectTaskId, x.DateModified });
                    table.ForeignKey(
                        name: "FK_ProjectTaskStatusModifications_ProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetActivities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IpAdd = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    TimeSheetTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetActivities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheetActivities_IpAddresses_IpAdd",
                        column: x => x.IpAdd,
                        principalTable: "IpAddresses",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetActivityLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IpAdd = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    TimeSheetActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetActivityLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheetActivityLogs_IpAddresses_IpAdd",
                        column: x => x.IpAdd,
                        principalTable: "IpAddresses",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheetActivityLogs_TimeSheetActivities_TimeSheetActivityId",
                        column: x => x.TimeSheetActivityId,
                        principalTable: "TimeSheetActivities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogging",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IpAdd = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    LogStatusCode = table.Column<short>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogging", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLogging_IpAddresses_IpAdd",
                        column: x => x.IpAdd,
                        principalTable: "IpAddresses",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamsProjects",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsProjects", x => new { x.ProjectId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamsProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetTasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeSheetId = table.Column<int>(nullable: false),
                    ProjectTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheetTasks_ProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Broadcasts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    NotificationTypeCode = table.Column<short>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: false),
                    FromUserId = table.Column<string>(nullable: true),
                    ToTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcasts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    AssignedByUserId = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    DateAssigned = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 30, nullable: true),
                    Title = table.Column<string>(maxLength: 60, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    MonthlySalary = table.Column<float>(nullable: true),
                    HourlyRate = table.Column<float>(nullable: true),
                    EmploymentTypeCode = table.Column<short>(nullable: true),
                    RoleCode = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    AddedByUserId = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Users_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AddedByUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Users_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_UserNotifications_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "EmploymentTypeCode", "FirstName", "HourlyRate", "LastName", "MiddleName", "MonthlySalary", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RoleCode", "SecurityStamp", "TeamId", "Title", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "c038fc8e-3043-4797-82db-ce8ba3542c75", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@sys.com", true, null, "Admin", null, null, null, null, "ADMIN@SYS.COM", "ADMIN", "AQAAAAEAACcQAAAAELKFOi3kQLerM47v90J4hn66CGNLXrJgFrWa/ZTCQ1Zi1ocgMD47hwf1XfW24M/qpw==", (short)2, "", null, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "EmploymentTypeCode", "FirstName", "HourlyRate", "LastName", "MiddleName", "MonthlySalary", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RoleCode", "SecurityStamp", "TeamId", "Title", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e576", "7fb0b1f4-a623-42ce-973b-8ecbc40917d0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alikleitcr7@gmail.com", true, null, "Ali", null, "Kleit", null, null, "ALIKLEITCR7@GMAIL.COM", "ALIKLEIT", "AQAAAAEAACcQAAAAEOA+icTE9sog77SmDC5yhomE4DHn7iT0+bVKn8ST/ljaWF2i8kZYzxhCZLWGbZ2LYQ==", (short)1, "", null, "Developer", "alikleit" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_FromUserId",
                table: "Broadcasts",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_ToTeamId",
                table: "Broadcasts",
                column: "ToTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AddedByUserId",
                table: "Projects",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_AssignedByUserId",
                table: "Supervisor",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_TeamId",
                table: "Supervisor",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_UserId",
                table: "Supervisor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AddedByUserId",
                table: "Teams",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsProjects_TeamId",
                table: "TeamsProjects",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivities_IpAdd",
                table: "TimeSheetActivities",
                column: "IpAdd");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivities_TimeSheetTaskId",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivityLogs_IpAdd",
                table: "TimeSheetActivityLogs",
                column: "IpAdd");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivityLogs_TimeSheetActivityId",
                table: "TimeSheetActivityLogs",
                column: "TimeSheetActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_AddedByUserId",
                table: "TimeSheets",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_UserId",
                table: "TimeSheets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetTasks_ProjectTaskId",
                table: "TimeSheetTasks",
                column: "ProjectTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetTasks_TimeSheetId",
                table: "TimeSheetTasks",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogging_IpAdd",
                table: "UserLogging",
                column: "IpAdd");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogging_UserId",
                table: "UserLogging",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_FromUserId",
                table: "UserNotifications",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ToUserId",
                table: "UserNotifications",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_AddedByUserId",
                table: "Projects",
                column: "AddedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskId",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskId",
                principalTable: "TimeSheetTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogging_Users_UserId",
                table: "UserLogging",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsProjects_Teams_TeamId",
                table: "TeamsProjects",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetTasks_TimeSheets_TimeSheetId",
                table: "TimeSheetTasks",
                column: "TimeSheetId",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Users_FromUserId",
                table: "Broadcasts",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Teams_ToTeamId",
                table: "Broadcasts",
                column: "ToTeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Users_AssignedByUserId",
                table: "Supervisor",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Users_UserId",
                table: "Supervisor",
                column: "UserId",
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
                name: "FK_Users_Teams_TeamId",
                table: "Users",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_AddedByUserId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Broadcasts");

            migrationBuilder.DropTable(
                name: "ProjectStatusModifications");

            migrationBuilder.DropTable(
                name: "ProjectTaskStatusModifications");

            migrationBuilder.DropTable(
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "TeamsProjects");

            migrationBuilder.DropTable(
                name: "TimeSheetActivityLogs");

            migrationBuilder.DropTable(
                name: "UserLogging");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "TimeSheetActivities");

            migrationBuilder.DropTable(
                name: "IpAddresses");

            migrationBuilder.DropTable(
                name: "TimeSheetTasks");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
