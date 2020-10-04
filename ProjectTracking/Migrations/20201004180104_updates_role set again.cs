using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class updates_rolesetagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_Address",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_Address",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_Address",
                table: "UserLogging");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e572" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e576", "a18be9c0-aa65-4af8-bd17-00bd9344e590" });

            migrationBuilder.DropColumn(
                name: "TimeSheetProjectTaskId",
                table: "TimeSheetActivities");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "UserLogging",
                newName: "IpAdd");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogging_Address",
                table: "UserLogging",
                newName: "IX_UserLogging_IpAdd");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TimeSheetActivityLogs",
                newName: "IpAdd");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivityLogs_Address",
                table: "TimeSheetActivityLogs",
                newName: "IX_TimeSheetActivityLogs_IpAdd");

            migrationBuilder.RenameColumn(
                name: "TimeSheetTaskID",
                table: "TimeSheetActivities",
                newName: "TimeSheetTaskId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TimeSheetActivities",
                newName: "IpAdd");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivities_TimeSheetTaskID",
                table: "TimeSheetActivities",
                newName: "IX_TimeSheetActivities_TimeSheetTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivities_Address",
                table: "TimeSheetActivities",
                newName: "IX_TimeSheetActivities_IpAdd");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSheetTaskId",
                table: "TimeSheetActivities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "StatusCode",
                table: "ProjectTaskStatusModifications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "StatusCode",
                table: "ProjectTasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "StatusCode",
                table: "ProjectStatusModifications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "StatusCode",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId" },
                values: new object[] { "fc49aebf-b080-4d00-b16c-1a5ae58b2b92", "AQAAAAEAACcQAAAAEDmupmIgochdZIuOK79PpMeC93ulTnpiOOaeJHGWVNRKAWOaMSwr1nYy+BH7zMh9uQ==", "a18be9c0-aa65-4af8-bd17-00bd9344e572" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId" },
                values: new object[] { "6c5aa906-3923-4804-a9c1-bde9ced06ff2", "AQAAAAEAACcQAAAAEMokMMD+q3hHsDDEmpy7JOfFBZhJomfzCax3vsin2bqLgX/zMjaCeEqAhCFvlzCX2g==", "a18be9c0-aa65-4af8-bd17-00bd9344e590" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_IpAdd",
                table: "TimeSheetActivities",
                column: "IpAdd",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskId",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskId",
                principalTable: "TimeSheetTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_IpAdd",
                table: "TimeSheetActivityLogs",
                column: "IpAdd",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging",
                column: "IpAdd",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_IpAdd",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskId",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_IpAdd",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAdd",
                table: "UserLogging");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IpAdd",
                table: "UserLogging",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogging_IpAdd",
                table: "UserLogging",
                newName: "IX_UserLogging_Address");

            migrationBuilder.RenameColumn(
                name: "IpAdd",
                table: "TimeSheetActivityLogs",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivityLogs_IpAdd",
                table: "TimeSheetActivityLogs",
                newName: "IX_TimeSheetActivityLogs_Address");

            migrationBuilder.RenameColumn(
                name: "TimeSheetTaskId",
                table: "TimeSheetActivities",
                newName: "TimeSheetTaskID");

            migrationBuilder.RenameColumn(
                name: "IpAdd",
                table: "TimeSheetActivities",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivities_TimeSheetTaskId",
                table: "TimeSheetActivities",
                newName: "IX_TimeSheetActivities_TimeSheetTaskID");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSheetActivities_IpAdd",
                table: "TimeSheetActivities",
                newName: "IX_TimeSheetActivities_Address");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSheetTaskID",
                table: "TimeSheetActivities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TimeSheetProjectTaskId",
                table: "TimeSheetActivities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StatusCode",
                table: "ProjectTaskStatusModifications",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "StatusCode",
                table: "ProjectTasks",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "StatusCode",
                table: "ProjectStatusModifications",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "StatusCode",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "d1c2c2f0-18c5-4911-9939-c55321802e8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "05fb03eb-11e1-4af4-b870-3d4081516964");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e572" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e576", "a18be9c0-aa65-4af8-bd17-00bd9344e590" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ec39bba9-d916-4ee4-88a2-74e5e47fb63d", "AQAAAAEAACcQAAAAEOnxoq8/rq3HmpmYmH0+lIZlhgYMkIP3Zxyc+A6IaPctTsFL+86cG6i17JSz0dOhwA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01802a18-3dec-4296-9c7f-e77e076afbd8", "AQAAAAEAACcQAAAAEKxv/Ohv7Agm8iJEq/jtMJ14FuXrH6ASrehsc+/BfNWDT9KxSL2kt8w0tIcGwN8xVA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_Address",
                table: "TimeSheetActivities",
                column: "Address",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_TimeSheetTasks_TimeSheetTaskID",
                table: "TimeSheetActivities",
                column: "TimeSheetTaskID",
                principalTable: "TimeSheetTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_Address",
                table: "TimeSheetActivityLogs",
                column: "Address",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogging_IpAddresses_Address",
                table: "UserLogging",
                column: "Address",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
