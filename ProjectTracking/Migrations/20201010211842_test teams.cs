using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class testteams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f66ef0f0-2271-4775-b5e4-0b4bd33642e5", "AQAAAAEAACcQAAAAEA5/Pu4/jssScPv53j8810Mkw30+YDr/jsva+oX10kf7R9nteSu+/r607A7xHJausA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "52e8cdec-5097-413e-ade1-d34e80b85674", "AQAAAAEAACcQAAAAEEuzUuA72OMjbkBCZS0JI2GkS6XdlJTLjDazKKtPm9ABB2Hn86BAVg2cqXoJQBlwOw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "EmploymentTypeCode", "FirstName", "HourlyRate", "LastName", "MiddleName", "MonthlySalary", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RoleCode", "SecurityStamp", "TeamId", "Title", "UserName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e581", "491dd1e9-a1ca-43b1-b351-0d54b762b23d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mark@project-tracking.com", true, null, "Mark", null, "Goldman", null, null, "MARK@PROJECT-TRACKING.COM", "MARK", "AQAAAAEAACcQAAAAEDS9o4cKIBzRufrjYenBeEzCuky+51RGftvytQDPF6/onZXRh5+H4zXR6gzgpfbAHw==", (short)1, "", null, "Head IT", "mark" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e582", "7b4f2a7f-9e92-4b15-8bdb-4837c81c5521", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ashton@project-tracking.com", true, null, "Ashton", null, "Kutcher", null, null, "ASHTON@PROJECT-TRACKING.COM", "ASHTON", "AQAAAAEAACcQAAAAEFpG4MydoEwhLPxcwWBClrOSKGbdIuFhubVYTT/5AiP5YQkJlgbBETwyAhJv/mmx0Q==", (short)1, "", null, "Sr. Designer", "ashton" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e577", "32be3233-3a14-4949-82f2-f532f9ff663f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ted@project-tracking.com", true, null, "Ted", null, "Mosby", null, null, "TED@PROJECT-TRACKING.COM", "TED", "AQAAAAEAACcQAAAAEInTA/eveTGBd6yo8he7wSEyVH+y2MA2ktvel47nZGWxWctdZRO8/MQ60K31Ozf7OA==", (short)0, "", null, "Software Engineer", "ted" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e578", "6fe9f359-f3a9-4ebf-8c89-4a2f62dd50af", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "marshall@project-tracking.com", true, null, "Marshall", null, "Eriksen", null, null, "MARSHALL@PROJECT-TRACKING.COM", "MARSHALL", "AQAAAAEAACcQAAAAECl7SmWaYLnG3Gk+e+iliYw9vq1UwNpihCdH2/V2neTXq4BxuHgn9dWhAxk6R2Ls8Q==", (short)0, "", null, "Jr. Developer", "marshall" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e579", "d6cdf6c4-848a-4407-91b1-c64c5ac48ff7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lilly@project-tracking.com", true, null, "Lilly", null, "Aldrin", null, null, "LILLY@PROJECT-TRACKING.COM", "LILLY", "AQAAAAEAACcQAAAAEKZaEJh7a45Q/0g9SpnaLs+tmriTDt+H37uhgkHzHaZT/5oKkL2fgXZwcqGyGvuNDg==", (short)0, "", null, "Dev Leader", "lilly" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e580", "1b4a74f2-060f-4bd1-b63d-71f502ec6580", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "robin@project-tracking.com", true, null, "Robin", null, "Scherbatsky", null, null, "ROBIN@PROJECT-TRACKING.COM", "ROBIN", "AQAAAAEAACcQAAAAEBg2N+j2gjC0cAoPYjA2ZGLzUbOSFQxmy0BPfypIPtasi6vyfgXf28fAIErcdqkasQ==", (short)0, "", null, "Graphic Designer", "robin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e577", "32be3233-3a14-4949-82f2-f532f9ff663f" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e578", "6fe9f359-f3a9-4ebf-8c89-4a2f62dd50af" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e579", "d6cdf6c4-848a-4407-91b1-c64c5ac48ff7" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e580", "1b4a74f2-060f-4bd1-b63d-71f502ec6580" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e581", "491dd1e9-a1ca-43b1-b351-0d54b762b23d" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e582", "7b4f2a7f-9e92-4b15-8bdb-4837c81c5521" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f8f68f2-0c83-44dc-b493-844170ae3776", "AQAAAAEAACcQAAAAEAH2otD4wctid6jtU2HgqcXkRIDY6jyGwP0N5dwupaDhcZuiR+M1P8ndwYIOUiG72w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f10bba6-8ba2-4f65-999f-89a5cc3af249", "AQAAAAEAACcQAAAAELeP3rpHILU1HbgBgiLkN5ISX+FCSWTo6FDBhNn5eQciJ42w090lIYCKUwp+O0tp1A==" });
        }
    }
}
