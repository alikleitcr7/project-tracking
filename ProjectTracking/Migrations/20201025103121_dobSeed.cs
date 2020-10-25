using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class dobSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e119b367-892b-4d0f-94ba-be9d35b5f142", "AQAAAAEAACcQAAAAEHVFdT+UdxOBopnZv167AmKpLKqThGZpbbYnjN807MmonFvTBJjp39NFEBDdv9vkgw==", new DateTime(2020, 10, 25, 12, 31, 20, 862, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "20bed9f1-ee1b-48f2-98c9-a129dace667c", "AQAAAAEAACcQAAAAEFMa5VivxuhLXc2DOpwjr9tYJMl3yZrHwQLsZoFN71TAzMkA4tPoP3e0eFeVPLdgGg==", new DateTime(2020, 10, 25, 12, 31, 20, 871, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "55ef2185-d01e-431e-9700-14508570d5fe", new DateTime(1996, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEL4wsy7cMHQ4BZu3hyVwKX5ch/ASDz7kEgCISOv1CtyuuyxtRrlfJgTBVN7fH7CyIw==", new DateTime(2020, 10, 25, 12, 31, 20, 900, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2d18183a-5e70-406a-8401-c80c691b4ba0", new DateTime(1996, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAENAgCJiMy51kXZNSvGn9OjEY61NrLrrxP8ar+aVK7gchI3FcVN37RnyVMtrRV7WTzw==", new DateTime(2020, 10, 25, 12, 31, 20, 910, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "e373897f-feb7-48e0-9f77-52dc97fc453c", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEJEuBJ8bp2JMle8WGVYIh2e7tdbaSHnkjZP+QZ9MjNaoDgvq+8fEzNxpnV1z8Xuzww==", new DateTime(2020, 10, 25, 12, 31, 20, 918, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "19041f96-de53-4ecb-ba2a-4f3e397034b2", new DateTime(1996, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEDzAIwDx1obsVrq3y7RPwPID54OwyMqTlZz2IGkidbc1TrlEAUrJobD+GDMouYtJvA==", new DateTime(2020, 10, 25, 12, 31, 20, 927, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d46b3a95-c83b-4a10-9de0-edcdb62e0953", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEOF0vU5scJnOxdU5d6qyPOnorHmCdxOVn7ON/MRe7xMV8GSRLrqyd3usLAu9r2H8pw==", new DateTime(2020, 10, 25, 12, 31, 20, 879, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "8ea3364a-aaf2-471e-b671-c01b2a278012", new DateTime(1996, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAELQdK0UCfV/uSH2KYONZPeCfyashleWxeU30mhU2nuCEE9N8cWyTWG9Lwqopw6crTg==", new DateTime(2020, 10, 25, 12, 31, 20, 889, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fa8f9049-5bfb-40e0-9f73-d3be00973dc9", "AQAAAAEAACcQAAAAEOuDwlC8h2RL2ypvg+sQMwKTaftXtUwdt6iqxI8SZsw7aoBQ2mqpeZpv8k5v2uyY/g==", new DateTime(2020, 10, 25, 12, 19, 14, 880, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2596ed32-692b-4a79-8422-489e423ef4ba", "AQAAAAEAACcQAAAAEH8VTVw6IEhsivxexvW8PH+zJqMkURd1E9e8vQ8tV+5w9iu3CkyQmqAjZ5cyeY9TwQ==", new DateTime(2020, 10, 25, 12, 19, 14, 891, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c19ccc49-f799-4511-9ce6-aa09bc811530", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEIfsfvSwFBPn7xvouT56jDcoH/chUeXMmEVlQmlWNoLgrz8NYTpLWjL8wkdLDtjE5Q==", new DateTime(2020, 10, 25, 12, 19, 14, 923, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5e070fd3-2d94-4fd0-9d08-ca5a01227204", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEC2PvQzCnzIDnx67hAw8KV2CS3Z13PaRkkPH2qJnCl85b3AlNFc0Gkpujq5vwhMtmw==", new DateTime(2020, 10, 25, 12, 19, 14, 936, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "a85ea73b-d631-48ff-90e3-440312a8fd6f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEHSZtDuQWgoU3s6te/1CXGzP53w6q6h861Ufpc/5lBMDhY6afAxRLVkEBXnS3ULzFg==", new DateTime(2020, 10, 25, 12, 19, 14, 950, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7145be7a-3c79-4c68-b351-8670fe9484ea", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAECysQswZ+muoBU61vzkZEBKx9SF83Z52lf4lqZbf7hh0hVWPa05pbYBchkMWx9kaXg==", new DateTime(2020, 10, 25, 12, 19, 14, 964, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "50287cad-bbd5-42e1-8e12-aafa9633f899", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEJ7SWjwqlWK8ALzJonFOUkdfsnA/GxcF+n9s2EHaYc6jEjQOCocsj+r6Ea3WlM77aw==", new DateTime(2020, 10, 25, 12, 19, 14, 900, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "2bdca60b-7952-4726-b6b5-d398dada84da", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAED14m0fQxXc6+QRKMwEeGQtNGaXB1enTG6RS+EYzhtgw6QwdzvigB4daZLNsibLXWA==", new DateTime(2020, 10, 25, 12, 19, 14, 909, DateTimeKind.Local) });
        }
    }
}
