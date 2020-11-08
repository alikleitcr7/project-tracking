using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class statusbytoproj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "ProjectStatusModifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusByUserId",
                table: "Projects",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "9cd3e846-7b46-4c47-8e7e-e45021542ca0", "AQAAAAEAACcQAAAAECpCbZvsTd30vXTI2pNRbpSrz2OT1UEy/iwpm6PvNyOOD3gBrMBHvsiOD9kcTwR1hQ==", new DateTime(2020, 11, 8, 21, 44, 51, 341, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7f4bf033-c131-4982-8078-ad5d67cde831", "AQAAAAEAACcQAAAAEKyBKIf4DhL0oxvywUp3seUf6OiFzs9Oat0zWskSKMZg2tNGGnPaZdCaS25evBF7YQ==", new DateTime(2020, 11, 8, 21, 44, 51, 356, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "83023847-6318-4984-af32-083712eac705", "AQAAAAEAACcQAAAAEL0RVPbWE0ZvE58jZ8XcNLNOojKijggBUkjoPZrqCDAphJtoVlWF2CGAy2Z9rhk/XA==", new DateTime(2020, 11, 8, 21, 44, 51, 379, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "042d78ce-1a15-43b5-9e8b-f956b652c795", "AQAAAAEAACcQAAAAEHUXqf8A72hIIvEXgAakblowIXBpASyCsFaEdyMkXltgy71hN0BWIoY9aELZ3v2kfw==", new DateTime(2020, 11, 8, 21, 44, 51, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "a797b68e-e682-44ae-a5b6-77fa54071f47", "AQAAAAEAACcQAAAAEMFNd9Nt+pLFjCQxeG8ko61AmYTnULw0BadTTiog4iMVrfX+CdkkLss74z9iTLrL9g==", new DateTime(2020, 11, 8, 21, 44, 51, 393, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2a32c9cf-d1c3-46fb-ba97-22ff73236cd8", "AQAAAAEAACcQAAAAEKh9oQeCJrzyCdKqLbmnDFDiTTF9j07dKCQ8nEEvelUOhk7vssx6oVsRh1SzatsSdg==", new DateTime(2020, 11, 8, 21, 44, 51, 404, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "78a5f7be-bb07-431b-ab7d-6157e4280ba5", "AQAAAAEAACcQAAAAEDQ1t7ZcmALw3q4SF+GEZdYQEw9VEAweZkrPQJHLU1XSuj3hB8cbYd6J8YCwFBq6zA==", new DateTime(2020, 11, 8, 21, 44, 51, 365, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e7d4e8b2-6c1f-49b6-b606-942579352828", "AQAAAAEAACcQAAAAEP6Ik1ZVQHvVgEyeVdty1oItu8AErqW0FtOs736iIuUXwuImto13xHD2M43KkOjdHw==", new DateTime(2020, 11, 8, 21, 44, 51, 372, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStatusModifications_ModifiedByUserId",
                table: "ProjectStatusModifications",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StatusByUserId",
                table: "Projects",
                column: "StatusByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_StatusByUserId",
                table: "Projects",
                column: "StatusByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatusModifications_Users_ModifiedByUserId",
                table: "ProjectStatusModifications",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_StatusByUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStatusModifications_Users_ModifiedByUserId",
                table: "ProjectStatusModifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectStatusModifications_ModifiedByUserId",
                table: "ProjectStatusModifications");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StatusByUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "ProjectStatusModifications");

            migrationBuilder.DropColumn(
                name: "StatusByUserId",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "475e8dbf-155f-4029-b8da-6b0cc6ee7bfb", "AQAAAAEAACcQAAAAEKF0V09E0quijjgiwGA3IIXO/iESRoskdzjefGYy5CY0YNqLo/L2bJsoV0wv+cb6gw==", new DateTime(2020, 11, 8, 19, 6, 53, 687, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "35f9c332-d296-4a76-b5e6-09cd955f4681", "AQAAAAEAACcQAAAAEPURXYzIlHvUJxmYATcgbZdVanXA0eQcoa0eimFKeQbjw3nLOPZPfpZ19y1Sql4PkA==", new DateTime(2020, 11, 8, 19, 6, 53, 697, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "4ff270b3-5ce2-4697-91d5-67b468aae629", "AQAAAAEAACcQAAAAEM5mq6Y30eySgXzk8Gi6p6YOqOYWYkpABvjZrOwG5PDVGN1/nprkPRkuYRRjnNuG3g==", new DateTime(2020, 11, 8, 19, 6, 53, 726, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d2866659-ceb0-48f6-bbb8-8c64d0ef1720", "AQAAAAEAACcQAAAAEKJRJGyJ1CJkwBBX8ctHowQAtJrbUc2JxilJSGKLGaPHDfhuZo4KNgmhASp8UpE9zA==", new DateTime(2020, 11, 8, 19, 6, 53, 733, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f98e55bd-35ba-4734-a8dc-7402d0b823e9", "AQAAAAEAACcQAAAAENDGy0wLUBMpIZb/PP7hvV5L/jbOPQFOsu7ZMsc1YQPA5FJa+zKWEvxMijtVdZ0n+A==", new DateTime(2020, 11, 8, 19, 6, 53, 740, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "69cf3f60-ee23-4b75-8105-8f587df014ae", "AQAAAAEAACcQAAAAEO0WqLJf8vtoa9535WOKGtjwbdXM4IXw29489m785EKWZB9XATStWhAJ/4Tga8jEAA==", new DateTime(2020, 11, 8, 19, 6, 53, 746, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c3ff9545-215d-4c4d-be47-cf25c567d46c", "AQAAAAEAACcQAAAAEEtTDiD17MamPS0UUJ0FpdxxblIahXOfxUT9BZ3XfSie0+ugKt+DrVWDuLYatWAB2w==", new DateTime(2020, 11, 8, 19, 6, 53, 705, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5bc99ae1-f872-4833-9c22-b63e0a8c738e", "AQAAAAEAACcQAAAAENVkKIoaetkbynNXTWvRv86PTwksBX7DblxN4gBWqu23L07VB0P0xrYMBsvOAChi4w==", new DateTime(2020, 11, 8, 19, 6, 53, 716, DateTimeKind.Local) });
        }
    }
}
