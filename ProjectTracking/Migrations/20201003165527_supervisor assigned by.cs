using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class supervisorassignedby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AssignedByUserId",
                table: "Supervisers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAssigned",
                table: "Supervisers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572",
                column: "ConcurrencyStamp",
                value: "74eab47c-bfeb-4781-a205-23fd528a3b7b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e590",
                column: "ConcurrencyStamp",
                value: "ce7b4719-790b-4dfa-adb2-620193206127");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82e24a60-47e8-4af6-86e6-e64714ac5eaa", "AQAAAAEAACcQAAAAEKdRaRKv7F0RnOqCgsjQ4dmzec5DMT3xqi7JQBACJ5M2idMC61nhvhYfmchIFlsN8g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AddedByUserId",
                table: "Teams",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisers_AssignedByUserId",
                table: "Supervisers",
                column: "AssignedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisers_AspNetUsers_AssignedByUserId",
                table: "Supervisers",
                column: "AssignedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_AddedByUserId",
                table: "Teams",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisers_AspNetUsers_AssignedByUserId",
                table: "Supervisers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_AddedByUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AddedByUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Supervisers_AssignedByUserId",
                table: "Supervisers");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AssignedByUserId",
                table: "Supervisers");

            migrationBuilder.DropColumn(
                name: "DateAssigned",
                table: "Supervisers");

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
        }
    }
}
