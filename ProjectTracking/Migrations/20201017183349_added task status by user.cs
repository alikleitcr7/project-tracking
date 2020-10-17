using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class addedtaskstatusbyuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "ProjectTaskStatusModifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusByUserId",
                table: "ProjectTasks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0aa11c2f-0601-4720-82f3-7c8f4acdddcd", "AQAAAAEAACcQAAAAEGPe/SENni0ylXwgrod9V4s4IDS6DzlIuk2VG2GH5LaaUwMax/Vcj7nrjlW4xdp+xA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bdff4c3b-603f-43f9-9a33-4888a09e388b", "AQAAAAEAACcQAAAAEEVd7zEOP9RrfOV8E1amr4yogQ1Je4YNBBrJWhBcCZh+AdIERJuFQnE/mNh0BQYUEA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b224825-ed3c-4433-abfa-c28171c0c1e5", "AQAAAAEAACcQAAAAEJn4p2+VOcehGVMRswA1iWuFKNfMDO+i+5oAAgBNwMTiOSYMHYKw34ITmg+sfQrHRw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "811724a5-5f3f-4f71-860b-f6fd9b022c5a", "AQAAAAEAACcQAAAAENLItWJMCvGMGEcBY6gaNIIERMgO181YP9uuar/+1dAgpLyQtL0hMGoi1tKIKL7x7Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "72ca6457-e10c-4452-85cb-cbaed30b52cd", "AQAAAAEAACcQAAAAEIPRCSZAEthhxBnwbRfChT/s8QVlMQ5hihjuK/VExVaPM54bq8/LNsBiaCwlcMEG4g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d144bd91-4b78-4110-8e7f-4331d7cde5be", "AQAAAAEAACcQAAAAEPvRVf2SFa3f0rjE88NiIeC/ha9hpBMXz/KNiIiv2Cn/v3rzk/uEzPOTyldLCBGsAQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0fcdefd8-d786-4aea-bed2-228256539e39", "AQAAAAEAACcQAAAAEJoNrXNLhHUlRhIUhIi0YZoCE+DUG0J98aQasc1mkdG/Qth+QOwIM/h8adoq+8NNag==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca436ed8-e748-4d22-8199-bfb337bfe529", "AQAAAAEAACcQAAAAECoj+4JcVYeZ92xE3v4yBY+v5xlh0XIPg9bvmHwHIJDY5Q+VYb07+Mcg6vgoQIiXZA==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskStatusModifications_ModifiedByUserId",
                table: "ProjectTaskStatusModifications",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_StatusByUserId",
                table: "ProjectTasks",
                column: "StatusByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Users_StatusByUserId",
                table: "ProjectTasks",
                column: "StatusByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTaskStatusModifications_Users_ModifiedByUserId",
                table: "ProjectTaskStatusModifications",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Users_StatusByUserId",
                table: "ProjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTaskStatusModifications_Users_ModifiedByUserId",
                table: "ProjectTaskStatusModifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTaskStatusModifications_ModifiedByUserId",
                table: "ProjectTaskStatusModifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_StatusByUserId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "ProjectTaskStatusModifications");

            migrationBuilder.DropColumn(
                name: "StatusByUserId",
                table: "ProjectTasks");

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
    }
}
