using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarListApp.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingDanielAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45d8f472-a7b5-46df-b585-877b7fed6853",
                column: "ConcurrencyStamp",
                value: "806d53b5-5a8d-43e3-8c2e-150006233d39");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66dec4b4-76b9-4d2f-abf9-429038afe3aa",
                column: "ConcurrencyStamp",
                value: "f57b0baf-a00f-4d6d-876a-3ef0b060f060");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "386ec0df-f833-4091-a145-1fee28253603",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2910fc6d-519c-4fa1-8583-a86a9b7a9505", "AQAAAAEAACcQAAAAEOhV/k97edpLJ7r0T+xkyregIvgqzbfoz22YhRKHel9fDe3Ym2Tw/NDCUmcbQ+dNBg==", "bc18a10c-699b-436d-8679-399aeb21e459" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56c168b6-904f-479e-a580-8949b3c394cc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf1b3e40-7bd4-437a-8818-8de04024d841", "AQAAAAEAACcQAAAAEOmegldzcq2cMrrL2quzjh6WO6+1/Q/2XOlmkrAjHIKvC+lybHZcQPvl2bh1so8LRQ==", "d636b791-b0ab-413a-88db-5550347ca4f2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "56c168b6-904f-479e-a580-8949b3c39412", 0, "6db02440-80aa-4347-af16-10c768076673", "daniel@localhost.com", true, false, null, "DANIEL@LOCALHOST.COM", "DANIEL@LOCALHOST.COM", "AQAAAAEAACcQAAAAEApI2sVn4YPi4PsVxmMoxHSwza3jt+K3JhdybAQpr3Qf8GusiBX7dKCZZCc5DNRs8g==", null, false, "1174a45f-87ea-4026-9a14-9210f5aac55a", false, "daniel@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "66dec4b4-76b9-4d2f-abf9-429038afe3aa", "56c168b6-904f-479e-a580-8949b3c39412" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "66dec4b4-76b9-4d2f-abf9-429038afe3aa", "56c168b6-904f-479e-a580-8949b3c39412" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56c168b6-904f-479e-a580-8949b3c39412");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45d8f472-a7b5-46df-b585-877b7fed6853",
                column: "ConcurrencyStamp",
                value: "50a22f67-676d-4aeb-a046-6acadba66c90");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66dec4b4-76b9-4d2f-abf9-429038afe3aa",
                column: "ConcurrencyStamp",
                value: "fa50f116-8198-42df-a823-677df6250a25");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "386ec0df-f833-4091-a145-1fee28253603",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d02ebe5e-75ac-4233-b327-2d8d06bc0054", "AQAAAAEAACcQAAAAEFRehD7pcKbmwIMezuxMuNZMxDrhHD+p50goDKSTEX3LDkOHrGfbejxYCMDyL1E4wQ==", "5e3c278f-fa82-4fb5-ad51-365ece8363f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56c168b6-904f-479e-a580-8949b3c394cc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6aed31d2-92ea-4538-b761-4c3603c8a6e1", "AQAAAAEAACcQAAAAEA3Wcs51pTl9Y2nIox+s/QKWc/JAqh/JRge2o7f4fpenqHGmrT63X1Z5ag+n3xuNUA==", "1085364e-ddad-41b0-8b90-86a1971c5bdc" });
        }
    }
}
