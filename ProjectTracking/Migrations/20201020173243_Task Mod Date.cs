using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class TaskModDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ProjectTasks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "44ab0d52-306c-4df0-9103-9c8620c82e3c", "AQAAAAEAACcQAAAAEMqy0La9MLXowL+YUWMHivc2VJt6/k9IhfIs/RKhYqqRDc1lcaBMqr6FhH6bGA3rZA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4b55efe1-0996-4ed8-bb53-f96e54e44ec8", "AQAAAAEAACcQAAAAEA+dJmFOLWEy6gsd4PE83SscvcBWFyswcwDN5H7hCsJnzyuqwIUDFRnQAfpxtrghLQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c025890e-0716-4c0d-ad36-ee28e5458c03", "AQAAAAEAACcQAAAAEEEtpxsgH+HbwUA8HV/kz7L4IwuCTmdni2y3gKGTZXnOgb40yzaq8Icc5HBiezKuWQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d001c6db-94e4-4387-a8f6-921970cca6cb", "AQAAAAEAACcQAAAAEP8Ke2/rp/09TnTMwisxTecQbNGeyL9do4pQxTvI+SbmAvoO8uZMN3n9875zQV3Xig==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9d8a8180-3ce9-412c-bde9-c0b17ec825da", "AQAAAAEAACcQAAAAEPuP3YqHVQwPIR/8+SuelqruiGPYOFbmQHNbZcxvIc5lfV/dAUpODGmaUvVKsC4GtA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "456ee20f-c94d-4f6b-ac8f-f88b49ba18c4", "AQAAAAEAACcQAAAAEL1K/yxiZQ5zoX4DEnXcrXlQdfLz128qPGLiVOUQfleTCT6qTOkRqB61a+94wrFzWQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7f8e67e-267e-4acd-9313-25425ee80dd1", "AQAAAAEAACcQAAAAEDueOKWFz+tGtw4gt3Lv6H2HxwpM9UCIt25p85pckIKhms9UULv152l+kAUIXsPhuA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f53c0a0-8bad-4272-ba5f-ab142eaf0618", "AQAAAAEAACcQAAAAEIv42EfBleMhkl3pP8wWg8Rp6lgPNdFW111hXQf7wv/rPgZRLbKnAl0gdQF0JbsipA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ProjectTasks");

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
        }
    }
}
