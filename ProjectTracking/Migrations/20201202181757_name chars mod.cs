using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class namecharsmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectTasks",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6d8232b7-63ca-41ea-8cc7-aaefe1366b5f", "AQAAAAEAACcQAAAAEDjjHrsu1J2QYgWdEfWKAEbKbo+ybYaUX3Jbr12cIFONsmEhX8SaqPf817OX7RdEDQ==", new DateTime(2020, 12, 2, 20, 17, 56, 836, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ef5c7cc0-efde-4344-b5f1-8210abfe8989", "AQAAAAEAACcQAAAAED12WDlb46qD/nHC2PJuAwJNWHKkUVRM5V7DHSHlG6tpht/kOhGxb9qbOUCR9vsuMQ==", new DateTime(2020, 12, 2, 20, 17, 56, 846, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7fe26e28-c128-418f-8007-271f9c566adc", "AQAAAAEAACcQAAAAEFQa2EpgC+YKdqXpvgQlhlipJyaKfens+0QJ1usNia2dZDvLOMQDxtwocvpxALB+5g==", new DateTime(2020, 12, 2, 20, 17, 56, 869, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6e271eb0-c33a-4385-bc9b-46af86b349ed", "AQAAAAEAACcQAAAAEGv394+Cs+aXFRLiFlN6p80IL5iP2YDD/N3AFK3PSw0++YQxOtLP7so4nomLf+B0NQ==", new DateTime(2020, 12, 2, 20, 17, 56, 876, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "df940b64-d8f5-419f-8190-708207e07ec4", "AQAAAAEAACcQAAAAEPmh1qOZ2jQQyS18opZIsxR0917USwNOKPRXoB0r44474rpeaLpbZcPlOmL+0MUt3Q==", new DateTime(2020, 12, 2, 20, 17, 56, 887, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2439b92e-d19f-4062-bb12-ecff0f84db53", "AQAAAAEAACcQAAAAEHtCgoaM5LV9W2cObEq4FAKERlHi7vxcQXz5k7/glCQNpHnnY0uQQQmiVG+S14U3Ng==", new DateTime(2020, 12, 2, 20, 17, 56, 901, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5bc02aee-a15e-4b6b-affc-05bc9d0f2dce", "AQAAAAEAACcQAAAAEK0fueqYxpANohAuzW0WPubLMAzIzmZtA5Tbl/Hrsu8cRJUL/BTJ/vfvUtTYrimiOg==", new DateTime(2020, 12, 2, 20, 17, 56, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f22006d2-eded-4bcc-bf11-979405cd2f9d", "AQAAAAEAACcQAAAAEBjC8Ax7+sK29l7m8z2Ghp+gasL08WRbqFtEjNqtfwPbPEm/n/9o+VEeUxbay6D9dw==", new DateTime(2020, 12, 2, 20, 17, 56, 862, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectTasks",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "bf3a59e5-efed-4d1d-9d6a-9708367406b3", "AQAAAAEAACcQAAAAEPJ32VsWXMwdDG9osLYIVRoQ+ATbFkmXOWcxzLtzY3MPOHzRh+ymmvhdX6iVEGIngA==", new DateTime(2020, 12, 1, 21, 20, 30, 276, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d516d6c6-27f8-4ad3-ab0e-a5c79bd3ca55", "AQAAAAEAACcQAAAAEH4VMzdhl6pg/HVixcsOQWSogw/xTGOaJTBENpJtmFvyBdp3kewAeF/YzZB30lr1QA==", new DateTime(2020, 12, 1, 21, 20, 30, 287, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "67797719-7903-4320-8b82-e5473d1edf8a", "AQAAAAEAACcQAAAAEFktJVaDRBqTymdUyV9xbsZRVemoDN46wTMj1itp99mN4fPC/inu6+fJa5MGUMqyHA==", new DateTime(2020, 12, 1, 21, 20, 30, 313, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6c0badbc-638d-4fc3-b823-18768158d213", "AQAAAAEAACcQAAAAEG4ph6gCoIJ7HUGhH3z5FI9P7fr3Tst3W1fOkd3Jr2quaFWb82diezwYy/m8cA2bLQ==", new DateTime(2020, 12, 1, 21, 20, 30, 324, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1487b3f3-0433-41b5-b84d-6517a28b83be", "AQAAAAEAACcQAAAAEGLJGKF/dtnDLieG8TUq8eYfegPo2jV/yfuZrC/xBJrbZizMRySqYDD3PTsKRyuckQ==", new DateTime(2020, 12, 1, 21, 20, 30, 331, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "f2f24b98-0d13-4f63-85fe-0abfba15293e", "AQAAAAEAACcQAAAAEDg5wIGBaKUUv6lH2IM9xbxfNC5O4vc2oOlaGQKWZDB2vah5iY5vXA/ytX/PhomO5w==", new DateTime(2020, 12, 1, 21, 20, 30, 338, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c7d78140-17cd-401c-ae84-6ce0e8a07749", "AQAAAAEAACcQAAAAEJKttUm7Gmhyr7iJpcXr2mxs0f7Q9hOh0r5hnfp+CduL6qzeO55vYh32/ZuH54w1og==", new DateTime(2020, 12, 1, 21, 20, 30, 298, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ad302989-832f-4ebb-a099-715ede62a802", "AQAAAAEAACcQAAAAEB6aVV6egwesMcKPMhmdzeC1oiwnefnYKS1I8AZtrZ1OxDPDrEillUOaDPe6sDtyrg==", new DateTime(2020, 12, 1, 21, 20, 30, 306, DateTimeKind.Local) });
        }
    }
}
