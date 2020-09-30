using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class ipaddr_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_IpAddressID",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_IpAddressID",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAddressID",
                table: "UserLogging");

            migrationBuilder.DropIndex(
                name: "IX_UserLogging_IpAddressID",
                table: "UserLogging");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheetActivityLogs_IpAddressID",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheetActivities_IpAddressID",
                table: "TimeSheetActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses");

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "UserLogging");

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "TimeSheetActivities");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "IpAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UserLogging",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TimeSheetActivityLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TimeSheetActivities",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "IpAddresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses",
                column: "Address");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "625471f2-7829-49e8-9173-24fba8313161");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "adb5e88d-124d-496e-9ce9-f77b5c5a1e86");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "83028c0f-6643-4361-a834-98a0ae21f8d8", "AQAAAAEAACcQAAAAEOWbjsCXtQNQB8S8fuKQ7djrEcNbEcoWIOCQXANLunZlEBhJX6dxAg9jedbD0W7pgg==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogging_Address",
                table: "UserLogging",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivityLogs_Address",
                table: "TimeSheetActivityLogs",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivities_Address",
                table: "TimeSheetActivities",
                column: "Address");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_Address",
                table: "TimeSheetActivities",
                column: "Address",
                principalTable: "IpAddresses",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_Address",
                table: "TimeSheetActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_Address",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogging_IpAddresses_Address",
                table: "UserLogging");

            migrationBuilder.DropIndex(
                name: "IX_UserLogging_Address",
                table: "UserLogging");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheetActivityLogs_Address",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheetActivities_Address",
                table: "TimeSheetActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UserLogging",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "UserLogging",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TimeSheetActivityLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "TimeSheetActivityLogs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TimeSheetActivities",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "TimeSheetActivities",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "IpAddresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "IpAddresses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "3145629a-9351-4491-8e07-ee5b7266814d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "8a77e5c0-9c23-493d-ac73-64002f194a05");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5318b80d-dfe3-4f88-98e3-cd2d9a1fd027", "AQAAAAEAACcQAAAAEEplZDs/4UY4JP9uvoZlWV2eVmZJtwZ3oy5LIVDNF3EMAc7sI2LHqpJVtZsJkvhkXA==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogging_IpAddressID",
                table: "UserLogging",
                column: "IpAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivityLogs_IpAddressID",
                table: "TimeSheetActivityLogs",
                column: "IpAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetActivities_IpAddressID",
                table: "TimeSheetActivities",
                column: "IpAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivities_IpAddresses_IpAddressID",
                table: "TimeSheetActivities",
                column: "IpAddressID",
                principalTable: "IpAddresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheetActivityLogs_IpAddresses_IpAddressID",
                table: "TimeSheetActivityLogs",
                column: "IpAddressID",
                principalTable: "IpAddresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogging_IpAddresses_IpAddressID",
                table: "UserLogging",
                column: "IpAddressID",
                principalTable: "IpAddresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
