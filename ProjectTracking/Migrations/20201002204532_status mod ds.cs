using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class statusmodds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStatusModification_Projects_ProjectId",
                table: "ProjectStatusModification");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskStatusModification_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskStatusModification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTaskStatusModification",
                table: "ProjectTaskStatusModification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectStatusModification",
                table: "ProjectStatusModification");

            migrationBuilder.RenameTable(
                name: "ProjectTaskStatusModification",
                newName: "ProjectTaskStatusModifications");

            migrationBuilder.RenameTable(
                name: "ProjectStatusModification",
                newName: "ProjectStatusModifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTaskStatusModifications",
                table: "ProjectTaskStatusModifications",
                columns: new[] { "ProjectTaskId", "DateModified" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectStatusModifications",
                table: "ProjectStatusModifications",
                columns: new[] { "ProjectId", "DateModified" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "2389ffc9-d7bb-4729-a248-1f3bbea54003");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "3738e834-1237-49aa-abb5-81814317bf91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "14aaf64e-8fa8-42b7-8238-d28deb5feb7c", "AQAAAAEAACcQAAAAEHzsQPKLh0+7OPou9wDnmabY8JhgA1/ZIGMoOJgp50e/7RmtG96kzADPKYkPxnlQfw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatusModifications_Projects_ProjectId",
                table: "ProjectStatusModifications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskStatusModifications_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskStatusModifications",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStatusModifications_Projects_ProjectId",
                table: "ProjectStatusModifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskStatusModifications_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskStatusModifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTaskStatusModifications",
                table: "ProjectTaskStatusModifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectStatusModifications",
                table: "ProjectStatusModifications");

            migrationBuilder.RenameTable(
                name: "ProjectTaskStatusModifications",
                newName: "ProjectTaskStatusModification");

            migrationBuilder.RenameTable(
                name: "ProjectStatusModifications",
                newName: "ProjectStatusModification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTaskStatusModification",
                table: "ProjectTaskStatusModification",
                columns: new[] { "ProjectTaskId", "DateModified" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectStatusModification",
                table: "ProjectStatusModification",
                columns: new[] { "ProjectId", "DateModified" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "50531e26-5e6b-4c31-82f0-3c6564c8c2cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "aad0253c-368d-449e-9693-d81b953cef98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1e1c7103-028e-4fa4-9ad5-d8cdcdb272cd", "AQAAAAEAACcQAAAAEB/zTBZ0ubhlbcYOTgHOxZ7X9FkirzXFaytyJIU0X7bo78xlIEAidXP+5+T+q5ZdzQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatusModification_Projects_ProjectId",
                table: "ProjectStatusModification",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskStatusModification_ProjectTasks_ProjectTaskId",
                table: "ProjectTaskStatusModification",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
