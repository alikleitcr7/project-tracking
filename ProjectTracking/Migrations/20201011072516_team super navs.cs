using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class teamsupernavs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d4c7028-3c25-4865-be5a-bbaabdc4576b", "AQAAAAEAACcQAAAAEFGJgRUU4pnnQjRUDwOpGmVz3R8G+eh8zL9eEVQma6eydyg/Vd5OU/e/QEH2IBjAUg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "638c4c7b-d0ae-4331-947b-9d11027e56ae", "AQAAAAEAACcQAAAAEPYTlu1ga9HYOWZDnz5638U0HVz2CRBUYvtsqxE/RtUGzISnBZGLnYygZwwFnQaPdQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79ea719a-b45b-456c-8a30-fe4be127a119", "AQAAAAEAACcQAAAAEMNlW8SbwiWoc8u/VLM81SNHQ3dlXwiUd1T6oTT4puKnEARTCO3X+2j110fiLHd8Mg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa1ede70-ee69-483e-a65f-5db647889c29", "AQAAAAEAACcQAAAAEGdGFtoak2Ts38ZuFg39FxZah/SzLeHAP2XpEHm1E6Hlf0abtBXWuDCgBOxRmaAOow==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2f9e3184-0955-4f8a-b90d-4f5b8398fb81", "AQAAAAEAACcQAAAAELGPDpW/o8wpZn8aKF1QQ1arvZiYSN9E6ivbsutP7TxYiblK+GY5EeSoiBjfMoFTlw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7527d03d-9ce0-43d0-b6e8-28a983ba4a31", "AQAAAAEAACcQAAAAEI8FNBMwPIvjMXcQMgw8ReAh3+c09PwMBrIrCyWKuqd1QbfT/sr4DaCP3Lrmazsj1Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eb786f2a-09c9-4fce-a9c2-d6df09777b37", "AQAAAAEAACcQAAAAEBlErHy/vVfVsJPY1kSSNole+8Ff3EIW6STU7SC7Ug1KmGnoScptQHUF5oFx500X/w==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6ffa129-7c51-487e-9e0e-86335a4768a2", "AQAAAAEAACcQAAAAECBFkCQG6rYSJ1pN40xtjOpcCCgAmE8n6nEFhObWs5jtjUsB+0sSUNHH6VjN2X+pAA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AssignedByUserId",
                table: "Teams",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SupervisorId",
                table: "Teams",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_AssignedByUserId",
                table: "Teams",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_SupervisorId",
                table: "Teams",
                column: "SupervisorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_AssignedByUserId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_SupervisorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AssignedByUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_SupervisorId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedByUserId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "32be3233-3a14-4949-82f2-f532f9ff663f", "AQAAAAEAACcQAAAAEInTA/eveTGBd6yo8he7wSEyVH+y2MA2ktvel47nZGWxWctdZRO8/MQ60K31Ozf7OA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6fe9f359-f3a9-4ebf-8c89-4a2f62dd50af", "AQAAAAEAACcQAAAAECl7SmWaYLnG3Gk+e+iliYw9vq1UwNpihCdH2/V2neTXq4BxuHgn9dWhAxk6R2Ls8Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6cdf6c4-848a-4407-91b1-c64c5ac48ff7", "AQAAAAEAACcQAAAAEKZaEJh7a45Q/0g9SpnaLs+tmriTDt+H37uhgkHzHaZT/5oKkL2fgXZwcqGyGvuNDg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b4a74f2-060f-4bd1-b63d-71f502ec6580", "AQAAAAEAACcQAAAAEBg2N+j2gjC0cAoPYjA2ZGLzUbOSFQxmy0BPfypIPtasi6vyfgXf28fAIErcdqkasQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "491dd1e9-a1ca-43b1-b351-0d54b762b23d", "AQAAAAEAACcQAAAAEDS9o4cKIBzRufrjYenBeEzCuky+51RGftvytQDPF6/onZXRh5+H4zXR6gzgpfbAHw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7b4f2a7f-9e92-4b15-8bdb-4837c81c5521", "AQAAAAEAACcQAAAAEFpG4MydoEwhLPxcwWBClrOSKGbdIuFhubVYTT/5AiP5YQkJlgbBETwyAhJv/mmx0Q==" });
        }
    }
}
