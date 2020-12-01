using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class removedsalaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonthlySalary",
                table: "Users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "HourlyRate",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MonthlySalary",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "309df6c5-bb2c-40c3-bf8a-5b8b87ae4b48", "AQAAAAEAACcQAAAAEARMWuW3ei/MS45uKFbn4iemlWtfr9DdME9dVf1dhBilB2JtiJfYNz6BD6SYNg/MnQ==", new DateTime(2020, 11, 21, 22, 51, 1, 200, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "0c74c91a-3176-4c53-b54c-ebdea1e076c0", "AQAAAAEAACcQAAAAEIN6nGwSf8PEjbMrD8r/BtHmvtcMVPf58fc0sV44KQpx24d9jp+RNMUseIb32RttCQ==", new DateTime(2020, 11, 21, 22, 51, 1, 209, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "55adec1d-f66c-4b69-afd5-c9147dcd277c", "AQAAAAEAACcQAAAAELGHPH+s0HxZvlV5Ja4P5QxI7MLjwwJBFl9awBxQxgUdjE3JOTlixLUtWz+okSuP8Q==", new DateTime(2020, 11, 21, 22, 51, 1, 231, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "4ab4330e-2332-4243-ad11-a8bf66ba704d", "AQAAAAEAACcQAAAAEFu1drs3XNBFh5AeLLW5OpQ+GlFDyjmAQcr7CjonQL2U4a4rdhFvSh97s6wd8xq15g==", new DateTime(2020, 11, 21, 22, 51, 1, 238, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "6fd614d0-b13c-49a8-88f6-bd578e65c8da", "AQAAAAEAACcQAAAAEIkm69XhWj8A1guxhiSXlx9rzjsw6Pud7F8KbM57qnYIu6pPEfeaKi+iT/8VWLzUGA==", new DateTime(2020, 11, 21, 22, 51, 1, 244, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "8f52425a-ea92-4a25-9dab-9591c4f772ad", "AQAAAAEAACcQAAAAEDYFO65Wd2mdWMru2yqpIaAr72PiAr/uoZVJw4w1iai1cAxH8iaF4jLPRQIoGLZVIA==", new DateTime(2020, 11, 21, 22, 51, 1, 251, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "052df201-9bfd-47e5-b485-349e89fa2740", "AQAAAAEAACcQAAAAEMkspJ+ZgHeeiHObX7ejE/FoWOG/zyhK8+wvo+dXnwXiOX64dlAcOZAnxzO/BIsylQ==", new DateTime(2020, 11, 21, 22, 51, 1, 217, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleAssignedDate" },
                values: new object[] { "9b5a1a87-829f-46e9-98ad-2c6566ccfdef", "AQAAAAEAACcQAAAAEBDP1nl+LoMxMcpYFRjpRvPWt9mXMW0qd9KEerF1Ldhqz8yTj7NVpuLIQ+X6396KSQ==", new DateTime(2020, 11, 21, 22, 51, 1, 224, DateTimeKind.Local) });
        }
    }
}
