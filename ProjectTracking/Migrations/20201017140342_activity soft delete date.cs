using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class activitysoftdeletedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TimeSheetActivities",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3cb29ab-1fff-4f84-b762-49d1dd789d7c", "AQAAAAEAACcQAAAAEBQgpJCfsbeXohr79Be4u4gy1EoUYK7JmRoSKJT4OR9ZvdbzXlUJ4mLQoZS3tmB3bw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4de62068-4f0a-421d-a1b8-f79f522a935f", "AQAAAAEAACcQAAAAEB9VzNsQyQnbd4hBDc3sZiUTCExJ21+xjueuULCiGxhbbdLxHGVXPJJjt5enHvSqFw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b72c880-f16f-4a9a-95f2-e0085199dc1f", "AQAAAAEAACcQAAAAEDxnFq6hdNi+Q/EdbuMfs/atr/7jkwRMoF/2gfZvHwewzQXJ22m3XKVsMcBqNTc7tw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6813dac0-675d-4873-b547-343db473e3b2", "AQAAAAEAACcQAAAAEB9FJrJPwdfg88yE2Kpdtns289tWncqz9+/iIhAsLUkUY6pEoN815F+g42Oeq66GlQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6cedbd3a-e7b7-420a-b071-01d93f4a9f7b", "AQAAAAEAACcQAAAAEOiKXogpZgqQNMdBxTphEk7rEMJU/sYj5QtuJ4xpgD0lyGKG5duI6qwzCmHgmrxSyg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3e691d0-e1c5-46d1-a186-1981e707d400", "AQAAAAEAACcQAAAAEIHXxgHeBBs6G3GGw8ONKZo4rhY3v8yYjKDvJPFN6xR7RWFpDD57oZfrQ92pC6rkRg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0a420ad6-d0eb-4db3-8fd6-6c53b14f839d", "AQAAAAEAACcQAAAAELGCCPF1tUH86qIAypLae37KAb0RKtx1CYXsazwZEk1cx3jf5XY+HwiiIDn/vrww1Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "13744582-968c-46de-b535-bc824fcfb3b1", "AQAAAAEAACcQAAAAEMTcG5bptTPrRqOkFCsWl55W9DcT0XtKLzwFaZp14Jw/TB0i3sSW4JYfCczurEyRGA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TimeSheetActivities");

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
        }
    }
}
