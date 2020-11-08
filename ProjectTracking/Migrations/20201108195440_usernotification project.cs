using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usernotificationproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UserNotifications",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d5f03700-28f8-4916-bdbe-fa4a0a604e45", "AQAAAAEAACcQAAAAEDRJ/eS+SOejdEVBwQXZnk0zgeckHs6bMMW1y+NwMtQ9RvYJwpVE1/M52haSJca7nw==", new DateTime(2020, 11, 8, 21, 54, 39, 888, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5ce781c0-d87f-4fe9-80c3-a5048da34960", "AQAAAAEAACcQAAAAENOP2ztrMyzAMzB/yNbcqKHT+25BeKH0ap7dmxEs+xKmZS8bC1uFWl2XeMtkUiQuKQ==", new DateTime(2020, 11, 8, 21, 54, 39, 902, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2ef88b23-1b63-43bc-bf6b-da73d1ee8637", "AQAAAAEAACcQAAAAEEXUwz0ZTTivtzm0u54ZfV50g6rhmkoxeK1QlQKgXMdUaEAeKdkJzEjnAovvS27YTA==", new DateTime(2020, 11, 8, 21, 54, 39, 930, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "46746f2e-8343-4bb8-bbda-d233ded895b4", "AQAAAAEAACcQAAAAEJqTAKySQm08ss75UxsZ9lYTSF925W1ROIjcUfnKynm/JUOJX9+haFlVwcf888Kc7A==", new DateTime(2020, 11, 8, 21, 54, 39, 938, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e7442f8b-187d-4407-aed3-b34c2a71ccd4", "AQAAAAEAACcQAAAAEPdqod2dNXn8FbP9Z/Q/TmakcptKd7IUkyeEpfA7BswAwQ1mT0ahIMLqjL0aEXjgBg==", new DateTime(2020, 11, 8, 21, 54, 39, 947, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "814e93de-5d25-46b6-bb88-61b2301d9f6a", "AQAAAAEAACcQAAAAEN9mO8psZz5Y/2tWgxLaPMxjlmnU/rofG5vg4GyxNPRb4NrcBGYbspHBHFHc2FTNyQ==", new DateTime(2020, 11, 8, 21, 54, 39, 959, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "734d6526-ba6e-45a0-832a-042039950ee0", "AQAAAAEAACcQAAAAEKT0AdM9Jxvccht8EjoNg3esWoyk+OnGxLVVNxeTG6NzWzC9rM0gNR4Bbi6f8du8Mw==", new DateTime(2020, 11, 8, 21, 54, 39, 911, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7b1fb066-5e01-4b5a-bfdd-045efff98de4", "AQAAAAEAACcQAAAAEKlBnIlrgmAOWl4W+OQkvPDyEKVTzMpCCee4mBO5HBolB4RBFwL9ZS8kSoGoX5qqgQ==", new DateTime(2020, 11, 8, 21, 54, 39, 918, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ProjectId",
                table: "UserNotifications",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Projects_ProjectId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_ProjectId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UserNotifications");

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
        }
    }
}
