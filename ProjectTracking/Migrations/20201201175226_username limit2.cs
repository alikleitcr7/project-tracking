using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usernamelimit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "0b603a60-83c3-450f-926b-88ba9aa44502", "AQAAAAEAACcQAAAAEP8qV8I6R8+EJxEr/cb06RFezwpA6gcn7MJLJtGwECZn+IfrJG8xMoGgjVRehc5BnQ==", new DateTime(2020, 12, 1, 19, 46, 53, 534, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d8bd2127-b2ff-4752-bdb1-94b8fc38b227", "AQAAAAEAACcQAAAAELFzqMVfs5RqL0jtPvZS8igXVd4HafCK3BU0GhKn/PzhIwtsA51Mj5vAuD6A4hHy+w==", new DateTime(2020, 12, 1, 19, 46, 53, 552, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "03d09c64-c34f-4a6a-becb-9a93553411c6", "AQAAAAEAACcQAAAAECX68VS/+BWL+XFB7cuglfuqEVpfJ+9bvWv+rVzPeK3o1tOvbuhXNg21G3Iu6YcrtQ==", new DateTime(2020, 12, 1, 19, 46, 53, 601, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "509e77fc-f304-4f30-bd2f-1f570910124c", "AQAAAAEAACcQAAAAEFPuXGayCe4QjLrZsD7SAxRniVrC5kxYIcqw/7NAB5tMyvml1K7q4vCIhOLl75VFrQ==", new DateTime(2020, 12, 1, 19, 46, 53, 631, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2bc99543-2c3f-4484-b901-4cff61ece465", "AQAAAAEAACcQAAAAEIGjkaXILIwh8tt9bQnS6pT3N63R0Wx52UHmJplKWqW3o6Xr8NNlKT5C/LvZh8UybQ==", new DateTime(2020, 12, 1, 19, 46, 53, 655, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "916717bc-323e-40f9-8a85-41740f43aa94", "AQAAAAEAACcQAAAAENp2TrW9FJ6pg1YlGoUdRwDo3//rvtWyjmwOdeV4dRBMHS8UZV6bgZqyHb2M2xSBHw==", new DateTime(2020, 12, 1, 19, 46, 53, 683, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "0aba80f3-9ab8-4807-9eba-84d38ec627bd", "AQAAAAEAACcQAAAAELCcjZMw/RA6Wh+sdkGsuf+jlrsjYjBBwAHIMTT/Lr+lOVrqEjDYX05r/Kp6dUyoJg==", new DateTime(2020, 12, 1, 19, 46, 53, 568, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "30a74253-9819-4d9b-a655-13cc079ee2bd", "AQAAAAEAACcQAAAAEH5p/00ol3Svg5AbmCfupznV53MFHnce3q3QKlT6KbfGYwlX/3dc61tzEmpLkAG5IQ==", new DateTime(2020, 12, 1, 19, 46, 53, 587, DateTimeKind.Local) });
        }
    }
}
