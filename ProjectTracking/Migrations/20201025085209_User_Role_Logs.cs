using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class User_Role_Logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleLogs",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AssignedByUserId = table.Column<string>(nullable: false),
                    RoleCode = table.Column<short>(nullable: false),
                    DateAssigned = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleLogs", x => new { x.UserId, x.DateAssigned });
                    table.ForeignKey(
                        name: "FK_UserRoleLogs_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoleLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "805e4bf9-073c-4a79-b1a5-82644051b85d", "AQAAAAEAACcQAAAAEEXdx7MrrhTXXLU65IpTCyJq7REN7TkzE7F2xx2nkQWqFx9mLPcnWQX/EdvAk4aFLg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7cb69094-49ea-4cb7-b5d7-6b8f89808adb", "AQAAAAEAACcQAAAAEOQjApP6JUfHAfsl7lQB4eNG0zu1KHNSwtjVz4VcU+B72pX/vjv9hOykvFhn1h/mcA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a493d95e-51d3-452a-a42e-61ba69c04008", "AQAAAAEAACcQAAAAELnq4l6+YpGup8qRVIbSqRcpOKNqUtRutHep5CXRsdSzH+yrXXjyv0RNVyNc7vC4xg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "574e07f1-642d-40fd-9a04-b0ef7c49c839", "AQAAAAEAACcQAAAAEMIwNWVYlQO/mYy2LCBqJJWcFIlUqng8UL4kvOFkk/4WvteyAJ7EKisXXiAyuaaBEA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fb09e74-5034-42b6-8cb0-387860d31fb7", "AQAAAAEAACcQAAAAEGghRZk3O4LL/fQwhhBEwk3c5//H+rXpCd4jssGpegMAhD6c291RRMoYqZiIcQVqRw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fed72d0-d535-481a-a075-0e86eb3097e9", "AQAAAAEAACcQAAAAEOwxmMKWC/LJSfbpGXC4fbx9x2pLPXC3+ybzqE+Fb29r1IyitWiGZMbEyijasVFSHQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71843d2c-94c8-4ffd-8c4f-5076b2a8951c", "AQAAAAEAACcQAAAAEAA7Q3HRGI98Su9+nd+58GE9nkhWkWQa0A4OcLBADVafeZUlFXi478CURid1sGzCIA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7cff19a-a3b8-4e9e-a9ad-4ae940188e6c", "AQAAAAEAACcQAAAAEMEz3lPsu9fBMOEewJIHEtccOZGcGYkGR7WWCYwRWiH8265AUrfl037DAOxhXzuhfg==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleLogs_AssignedByUserId",
                table: "UserRoleLogs",
                column: "AssignedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleLogs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "126ebb4c-323c-4c26-aeae-fe1bf22732ad", "AQAAAAEAACcQAAAAEMQK4NCI3K3V2dWXkSHvicNJ2d0d8WdXTAIYBjZA+I6aqHrIimDAFuf7mWUA5jXIYg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "43baf1c7-27bc-403a-b706-216450d82e52", "AQAAAAEAACcQAAAAEEebqgvowGNSGEAkdAsyZPjKpeKRLW2mOiTAqTEe+gip2ONHxGh6k9grvM6duNdGrw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3df8ec8-4199-4df8-8dfb-4e105a0ff671", "AQAAAAEAACcQAAAAECcnxVXSCZaA2+OlgzZPJ0iDqzIJdEz0dcL4B9SK7Dc+9/gHNClCODK9Ys/sCHOUEg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ae561ed-b993-49c5-8c8e-b7ae3f296649", "AQAAAAEAACcQAAAAEM/QRrIjYt3lU06PSbGhiFwijYeSxdapxnJLOkaVYJFw1IdDsXIsQ9mot2i6jIbxgw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d22e3a78-1970-498a-8cb2-336c4511705e", "AQAAAAEAACcQAAAAENlr+D0gMp8inKLdltUVp0fyYOIOAJ3Txf8k1ZDHkMyFwQ1cfPACAPpCY3QNT/0RIw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "708c0563-53b8-4743-97e9-95894886ceaa", "AQAAAAEAACcQAAAAEBDD0jNOVx2Woxxm37H7zSRId//l6f65QSyRUPMkW5jNvSneeBrOECidXXi8YnTnyQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bfec22f6-bd46-4ada-ba94-0d72f70c5a8e", "AQAAAAEAACcQAAAAEFnRbJmL280w98HzqpB6SoIDYJmdNBC/1ZvsOvV2iuhw+DHAfxN4tYp7ka0O0fjvTg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b76dc3d6-58a9-491d-bff9-63cc91e0f490", "AQAAAAEAACcQAAAAEHcXeox+894/RdOhQtAuNUIxdNPQPug6vXGakwJiPCmRwWm27g8/WZSmcZER+RDiRA==" });
        }
    }
}
