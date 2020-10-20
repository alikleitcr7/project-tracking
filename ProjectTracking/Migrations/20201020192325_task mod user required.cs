using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracking.Migrations
{
    public partial class taskmoduserrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedByUserId",
                table: "ProjectTaskStatusModifications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedByUserId",
                table: "ProjectTaskStatusModifications",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
