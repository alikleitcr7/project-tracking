using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usernotificationtask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "UserNotifications",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d6e4e40f-a690-4452-9b49-8666141562a1", "AQAAAAEAACcQAAAAEOFpH7npn12sdRRcXS7uGl/DWAK3fr1UaSD2lvwGZ4K17PlFY6w5P4YFKsX7+sgT6g==", new DateTime(2020, 11, 9, 21, 51, 28, 700, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "813ae601-7bb6-47ed-9185-022348fd36cc", "AQAAAAEAACcQAAAAEMJH+Ctu4sKgMYI+IawtjuufD2bZwdtLB/1i9iW8ExlXzTG+7aj6iUx/r46VgZvoAg==", new DateTime(2020, 11, 9, 21, 51, 28, 709, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "b2991de0-e963-49ee-82f3-579a2304d4fe", "AQAAAAEAACcQAAAAEOYbYFgF6wJb1Hl7WW+BadYIHptX9A4C10+wssQSJTnFjkuTI4hQvrp0YeKFVTrTRA==", new DateTime(2020, 11, 9, 21, 51, 28, 733, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "14a7c712-b6bb-4872-85fe-5875e8bbea65", "AQAAAAEAACcQAAAAEDhkEqGJKYf0rentbrGdSdZ7Ad2m5q0dq+BVdHqART9U4G0sIq+dUFpPSofxEbzuHg==", new DateTime(2020, 11, 9, 21, 51, 28, 739, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "624783e4-3b1a-47dc-a7bf-9c9fc259dd31", "AQAAAAEAACcQAAAAEE6ZZ01eEMKMB+1ANwLLpiRKfm6V3j8CNaipa4nZsaQ/jkElt31z69s6ns2B9IQI0Q==", new DateTime(2020, 11, 9, 21, 51, 28, 746, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f565deab-15a6-4e43-bbc8-8867f7247dd2", "AQAAAAEAACcQAAAAEBZbZ9M4rz1ZrQF2+jRa1IDVLGU6wZqEWVT4R4j0IBQTWTooj84sqhZgHXN1vyVcSg==", new DateTime(2020, 11, 9, 21, 51, 28, 754, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "829903c6-c5f9-44cb-89dd-fb4d2a3981da", "AQAAAAEAACcQAAAAEDNwwj41JVX27812u3JATD9WhOjUVOC8rzF9//ugnh7XGo5fjqB5Avf1Y5bIkVcwvg==", new DateTime(2020, 11, 9, 21, 51, 28, 718, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "304e28c0-45c2-4924-a1c9-6d4acd45f645", "AQAAAAEAACcQAAAAELuhsRmioVKSYRJBqg6eZbkA3p3jhGPfDlGEw4djcebzxOXKebd4+Qi/QnxWWjjp9A==", new DateTime(2020, 11, 9, 21, 51, 28, 724, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ProjectTaskId",
                table: "UserNotifications",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_ProjectTasks_ProjectTaskId",
                table: "UserNotifications",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_ProjectTasks_ProjectTaskId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_ProjectTaskId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "UserNotifications");

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
        }
    }
}
