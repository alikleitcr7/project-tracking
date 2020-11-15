using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class notifictionFalgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationFlag",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UserNotifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Broadcasts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7b9ae2a4-a1e7-41d7-a233-a0e9cdbc6a43", "AQAAAAEAACcQAAAAEF7Sf5Outd+2/hoSdx67r3VrA37g9GCfCE/Yq3rYdoerricqZifu9E4adz9FFc70BA==", new DateTime(2020, 11, 15, 11, 56, 30, 606, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "3ee5cfb0-58a7-4cf3-b908-964711978153", "AQAAAAEAACcQAAAAEDI9EH6mQaMzi9+cBEwvtHVFY7Z0hoabUsfVedOd24EZdoFG642tmjY+GsTc3n6WiA==", new DateTime(2020, 11, 15, 11, 56, 30, 615, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "49846b78-9199-46c2-8d18-be9e4c712890", "AQAAAAEAACcQAAAAEE3bU7r3lafUCWal7Pp9et4o1su/arcSsJUeqBX4tNU86oRT44288cGf3HXlN5X8Pw==", new DateTime(2020, 11, 15, 11, 56, 30, 636, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "c34c3d0d-b4e5-4050-8631-1c77ce719a3a", "AQAAAAEAACcQAAAAEDXOuSIJQh/7ih0lXEPuVOZKedgLSsHvCf8bNnvhrx1NzGVp/p8xyxQjxrRsU5jZEw==", new DateTime(2020, 11, 15, 11, 56, 30, 643, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "df6cecc8-36a1-449d-a392-ea282d7942ae", "AQAAAAEAACcQAAAAEL3bQ2q+Ur0+slcL6EPjHP3plOzZ3vUrtLQSGPqJ/SNTBCPu6YXgBePlTku3MYMNZQ==", new DateTime(2020, 11, 15, 11, 56, 30, 649, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "5f82c0bc-6609-4dc2-b615-bf9499903c0d", "AQAAAAEAACcQAAAAEEtFBqnBNH5cNB1AVOQZBzYW8StPYkdLmv90ZiENOyH6AJz6sgmZjmKwqZbODoHXDw==", new DateTime(2020, 11, 15, 11, 56, 30, 656, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "43c202d3-bc45-421f-8730-54e2bfa72b41", "AQAAAAEAACcQAAAAEF0565JHye8SHrnHBLOcrxyz1Cj5sx+LUT7ZCsyayzMqWFRMehqv6i27un4B4Tg1Ug==", new DateTime(2020, 11, 15, 11, 56, 30, 622, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fb796dd8-d0e2-46eb-ad1a-716627e65175", "AQAAAAEAACcQAAAAEBeC/k5s0z2wEoTtkZiadPTauCS/IFYOEyL+IVYAf5NgeXXi2itjBpaptkX/3fQWZA==", new DateTime(2020, 11, 15, 11, 56, 30, 629, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationFlag",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Broadcasts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "730b3e18-0381-4b30-9be0-a120500686d8", "AQAAAAEAACcQAAAAEOhyCW3XGoiIAFA3TZvIwlsPgt0D+PA8QY6s4ZHNTM195dVrqcQYLrUHjukt+EVK/w==", new DateTime(2020, 11, 10, 21, 57, 38, 365, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "ce04ff31-99c6-4c43-9539-25ed60183ec6", "AQAAAAEAACcQAAAAEJSx+aohd+9JQOMwGriFa+2SskN2M5RJEM+8dPLNaxnf4UqN8ggBodTS6tqogbtU+w==", new DateTime(2020, 11, 10, 21, 57, 38, 373, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "46a59529-58f1-4462-96a1-677210e19aa7", "AQAAAAEAACcQAAAAEE6nzF6y/58bPo33uOeCLj0y3eDI7ulUWAQG5k1ZIhwKUGlX6G46be/E4Sl94pTJWA==", new DateTime(2020, 11, 10, 21, 57, 38, 399, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "3aeec919-0041-4ecf-811d-757c45ee3538", "AQAAAAEAACcQAAAAEC0n1L6Z7FgeoEZJl1LjRc2JbOaE83jMoH1u/Sm5Kh1dOzb5MwJ558BCK3YdFn4YSA==", new DateTime(2020, 11, 10, 21, 57, 38, 405, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d6b88e31-b343-458e-a2c6-69ba60bf7d49", "AQAAAAEAACcQAAAAEP/xfraHq5zd+BopqiJr1r5xBzw3U0wWIPXgXCzUrATiqS0humcyLla3rwTjdDCt4w==", new DateTime(2020, 11, 10, 21, 57, 38, 412, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "d7f3d125-8e40-479c-8163-3217417a8631", "AQAAAAEAACcQAAAAEJCZOq+od/d6hTwG90S3FMssEzRldwNR1y6WXSkVAL2fCbA+oDcrWcRPU/OWxEsYFg==", new DateTime(2020, 11, 10, 21, 57, 38, 419, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "89e7fefb-bd0e-487a-8a60-d31f2494d085", "AQAAAAEAACcQAAAAEJuA6rKt4ZeDG0R5SrJf/UJQIIaUsQ6+fI2U1VWy5Y1H43k8TpbK2WalmjpdVSrSyg==", new DateTime(2020, 11, 10, 21, 57, 38, 385, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "9dd65704-67ee-47ed-8c17-feda933e232f", "AQAAAAEAACcQAAAAEFiYDkDNO20JYSlhQFlsO+m/r9AJpgIroeo6A7kTqx6/D2q2zTYolgp2JjqeNBlmUw==", new DateTime(2020, 11, 10, 21, 57, 38, 392, DateTimeKind.Local) });
        }
    }
}
