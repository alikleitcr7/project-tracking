using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class ProjectModDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Projects",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36fe8c88-0fe9-4a5e-8434-460934701892", "AQAAAAEAACcQAAAAENjOcbYRCeSJl7lhqYGVOLplJY3CEsBMvx+mYdOwvDRsmDKUZ4pGprRLyoq8Q/CKYw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e576",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a9f4d9e-ba64-41b4-adc0-ad20c2eaf1a5", "AQAAAAEAACcQAAAAEHSKpvptwvhPfh4hGwE+IKtx4kUwmoK7E3WkrzCpK+P/zvE9wmTRIA4Xcui5V2sNpw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e577",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cdf4bf86-66ad-4916-bd0c-0391a8e0d3c0", "AQAAAAEAACcQAAAAEBBfgaSYIMuVkf30B0yxZVVF3dKAulJWOOq9PDNf0WrO4VqaQwYxhjAcLB60gHRd/Q==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "405e011b-283d-4419-b03c-3b1b77aeb8c0", "AQAAAAEAACcQAAAAENTFU2uRhnqViZfy/eQ02aV91JFTj5rsSG66Phi5jhC3hLwi2qxcg5YwVkQh/Jt27g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "412fec32-6257-47fd-b9db-e087c82f6983", "AQAAAAEAACcQAAAAEPpdqhhOGom225yx/D5/M+gnckrP1pn9QmZJlr+Wqz98ADSs+jYTwoQ4gxkgfkQ6ow==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b4007f45-58d0-48c9-a10a-393fdd06a083", "AQAAAAEAACcQAAAAEKrC73i34eBmOgXr6iMinP/etyiHqot+seecHR1D1cAgtdvYN3k2TrEuXsff8x97Sw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e581",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ac8b8a4-2ed1-4728-a8c0-3cbc0bb92d67", "AQAAAAEAACcQAAAAENTiziQLIHWHsYAyRYAzruq33sOQDPDMao9TxoVRRS1jrzPkJd/Ikgx39iqULTZpCw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e582",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a186327-4246-4817-9f57-6754413f8f65", "AQAAAAEAACcQAAAAEFhEyQtZlBYAXMPPocbzwhVcvBgd+3jxXa2gJU0ZMUkAwHNC8xlfnCBkkx9gRElL5w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Projects");

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
    }
}
