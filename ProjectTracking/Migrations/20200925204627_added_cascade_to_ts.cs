using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class added_cascade_to_ts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "0fada57e-1a75-4c38-a326-95589c9b8d09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "535f4c2f-6137-4738-a6f0-ba153ba66962");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "beb81415-94d1-4c59-927c-cf2fae0c469b", "AQAAAAEAACcQAAAAECBcnkn02lj2cFXUE8q3RWi54pDugOxEkaWkNYnUTMZ+4wURSju0PEc6X9EY/q28HA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskID",
                principalTable: "TimeSheetTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "99950198-622d-4df0-9b8b-40fb8606c454");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "fa62f5a6-5ab8-45f1-bd0b-a75e5af57ae2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b7b50a0-43d1-4977-b022-5a0422335d7d", "AQAAAAEAACcQAAAAEKzGobS/bd5lvepsqSjpg/X8yegZYwEfDx2mVZ4cRg9s8Z8daodB/nDIC9rMMfT/Rw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskID",
                principalTable: "TimeSheetTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
