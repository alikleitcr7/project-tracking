using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class middlenamerem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Users",
                maxLength: 30,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5eb287da-e9ed-4898-bbc9-c6895a2bcf65", "AQAAAAEAACcQAAAAENuJSkN073VylMa1eIlMe0HmONspslDLSJZOcY5St3C4mTqQSNKVbUpdKV9Z+VIJ4g==", new DateTime(2020, 12, 1, 19, 52, 25, 671, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "817b15ac-240e-4be6-95a3-5faf1d5ecf81", "AQAAAAEAACcQAAAAEEG9q1fTqb03iAxThdu21B26GlI5iVQXPDO2Sc1/bCezK/WYJQtADFz+SQuMf/PXIw==", new DateTime(2020, 12, 1, 19, 52, 25, 680, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "50f9a24a-8649-485c-8390-75004f6f08a2", "AQAAAAEAACcQAAAAEI5ziA3zOQbpMsPcMFy8RgIRGwhKzpDwBdc+xt8u8goxPP7hw4WxSNqyqQKoB3erNw==", new DateTime(2020, 12, 1, 19, 52, 25, 703, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "acdccc32-16a4-4b41-a5f4-875bf15bad62", "AQAAAAEAACcQAAAAEKyQQkVyUStiA7KGIRycrc9P2mnBU4V0aupQoddumbbtuAS5i4T2cpFGhdwZ2MeG0Q==", new DateTime(2020, 12, 1, 19, 52, 25, 711, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fb6d52df-efef-4ae1-9700-c1ee18ec4bed", "AQAAAAEAACcQAAAAEAk/EeEZxuCiAxr8eOsEtSuDji7bFfan8mEsk9uB/GHUYwQBmNqM7Lm1eLxgNi5fhg==", new DateTime(2020, 12, 1, 19, 52, 25, 719, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "bae2b5fa-00a0-40f6-9db8-16de22a0e71f", "AQAAAAEAACcQAAAAEEgcyqeSeZMnGx1Z8YuSsDEP8BPvmq8SKyv7PkAG6WFGJMZrk1uIBnDSRZ48XuzAuQ==", new DateTime(2020, 12, 1, 19, 52, 25, 727, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "a8f79eb6-4066-411f-a43c-0a7b8d22350f", "AQAAAAEAACcQAAAAEBDaugCq11rFSmmdwKWE/xs7l2DT5b/mbDT7ygznLdJpIYhCxbAQ0jQkn3ltD97WIA==", new DateTime(2020, 12, 1, 19, 52, 25, 688, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e1cb79ec-74c1-4047-a3b6-2eac36971e41", "AQAAAAEAACcQAAAAEA8j2vYdK0g5sfGw8HunuWoc/wXqweWNC7Ct0P72s2l9tZAO2/bGiZae9Kd9lxWYZw==", new DateTime(2020, 12, 1, 19, 52, 25, 695, DateTimeKind.Local) });
        }
    }
}
