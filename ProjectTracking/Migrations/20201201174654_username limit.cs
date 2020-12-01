using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class usernamelimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "51a69990-ad6e-45fd-b936-cb35da056d56", "AQAAAAEAACcQAAAAEPvu4+PM3grE0Z3Hl/Ftxpzu93dnddh+iiwdSOYE4wLDJG3Jd3is8aTsadRKun8z8A==", new DateTime(2020, 12, 1, 12, 1, 15, 816, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "24a1a706-8eb8-41d3-aa52-6ca16068e5b8", "AQAAAAEAACcQAAAAEJn7/NTvSRLMZztuxD/26oDoRZG2p4akGUg5bO8pMwWlUxT97HCLFcesLj1ReoTzmA==", new DateTime(2020, 12, 1, 12, 1, 15, 825, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "7314171b-1643-4e36-b57a-36bd8069db8e", "AQAAAAEAACcQAAAAEHaaS8H6lU5uCni4ZUx8jaU6CcMcxl7lI5DHF2G9cz4WTNXEh0mHhVxNlR6OfxUpqg==", new DateTime(2020, 12, 1, 12, 1, 15, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fe93161c-8ff6-4181-be46-fdfec02638e8", "AQAAAAEAACcQAAAAECorA2uu4dCjEkJpFaAMA8VEJgEFnVjxPCp+THElGby9SGvawhOdjlAjyAGpdGaGuQ==", new DateTime(2020, 12, 1, 12, 1, 15, 863, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "08a88386-9600-4390-89e1-ab7ae4a99c59", "AQAAAAEAACcQAAAAEPSCXyKr1Qr+zvnqNgOLY8CiLJe1uy0bRZ4F1IXHZaj/6jI3tOJuNLEHd/JHcJLvww==", new DateTime(2020, 12, 1, 12, 1, 15, 870, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "fb47fe18-c462-4b3c-8877-6f345b30de2c", "AQAAAAEAACcQAAAAENrFS/vIr1bR1k7NjaYTrqeeRuHC54y8NT9zsIqLkuotBokSRji1fgOCir0uL/PGaQ==", new DateTime(2020, 12, 1, 12, 1, 15, 877, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "54c51775-f176-4a6e-8e24-19fe30f779ed", "AQAAAAEAACcQAAAAENq471LTkRuXfTTxHyAEDYriesYAOICy/4MGyaMxlK8tbXB7BJUr/aAwFQDlQvc7Hg==", new DateTime(2020, 12, 1, 12, 1, 15, 834, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "1891b68e-655f-4008-89c8-a234e0bfea2e", "AQAAAAEAACcQAAAAEBTYVtX30t2owiw7/BAxCR1wNE+pP6oKS7Zk4JQXdllgw2d0QdKh7JhBBRdiTWke4Q==", new DateTime(2020, 12, 1, 12, 1, 15, 844, DateTimeKind.Local) });
        }
    }
}
