using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarListApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45d8f472-a7b5-46df-b585-877b7fed6853", "50a22f67-676d-4aeb-a046-6acadba66c90", "User", "USER" },
                    { "66dec4b4-76b9-4d2f-abf9-429038afe3aa", "fa50f116-8198-42df-a823-677df6250a25", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "386ec0df-f833-4091-a145-1fee28253603", 0, "d02ebe5e-75ac-4233-b327-2d8d06bc0054", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAEFRehD7pcKbmwIMezuxMuNZMxDrhHD+p50goDKSTEX3LDkOHrGfbejxYCMDyL1E4wQ==", null, false, "5e3c278f-fa82-4fb5-ad51-365ece8363f8", false, "user@localhost.com" },
                    { "56c168b6-904f-479e-a580-8949b3c394cc", 0, "6aed31d2-92ea-4538-b761-4c3603c8a6e1", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEA3Wcs51pTl9Y2nIox+s/QKWc/JAqh/JRge2o7f4fpenqHGmrT63X1Z5ag+n3xuNUA==", null, false, "1085364e-ddad-41b0-8b90-86a1971c5bdc", false, "admin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "45d8f472-a7b5-46df-b585-877b7fed6853", "386ec0df-f833-4091-a145-1fee28253603" },
                    { "66dec4b4-76b9-4d2f-abf9-429038afe3aa", "56c168b6-904f-479e-a580-8949b3c394cc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45d8f472-a7b5-46df-b585-877b7fed6853", "386ec0df-f833-4091-a145-1fee28253603" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "66dec4b4-76b9-4d2f-abf9-429038afe3aa", "56c168b6-904f-479e-a580-8949b3c394cc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45d8f472-a7b5-46df-b585-877b7fed6853");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66dec4b4-76b9-4d2f-abf9-429038afe3aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "386ec0df-f833-4091-a145-1fee28253603");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56c168b6-904f-479e-a580-8949b3c394cc");
        }
    }
}
