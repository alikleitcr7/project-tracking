using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class addedipfks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IPAddress",
                table: "UserLogging",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "TimeSheetActivityLogs",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "TimeSheetActivities",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "UserLogging",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "TimeSheetActivityLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpAddressID",
                table: "TimeSheetActivities",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "UserLogging");

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "TimeSheetActivityLogs");

            migrationBuilder.DropColumn(
                name: "IpAddressID",
                table: "TimeSheetActivities");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "UserLogging",
                newName: "IPAddress");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TimeSheetActivityLogs",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TimeSheetActivities",
                newName: "IpAddress");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "6f378f90-1f13-4ca5-b78f-0eed88442ac7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "2635f72a-97ec-44d9-99e2-dca4b8d89bb1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "21099f96-abec-431e-a767-45eb42f45fdd", "AQAAAAEAACcQAAAAEPWHnt1RFa/olePzD81PeTxH3cJuoDYnkIE0LlPPjWKvOCKIMR/Lb0NxFY/XCERjcg==" });
        }
    }
}
