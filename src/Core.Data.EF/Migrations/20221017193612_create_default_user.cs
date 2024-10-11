using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Data.EF.Migrations
{
    public partial class create_default_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d9ffb18e-f9ad-4c60-9678-4526e371c0c3", 0, "ee45e2d7-9ab6-4a06-ac8b-973be5f8b27d", new DateTimeOffset(new DateTime(2022, 10, 17, 16, 36, 12, 779, DateTimeKind.Unspecified).AddTicks(47), new TimeSpan(0, -3, 0, 0, 0)), "juan@salvo.com", true, "Juan", "Salvo", true, new DateTimeOffset(new DateTime(2022, 10, 17, 16, 36, 12, 779, DateTimeKind.Unspecified).AddTicks(91), new TimeSpan(0, -3, 0, 0, 0)), "JUAN@SALVO.COM", "JUAN@SALVO.COM", "AQAAAAEAACcQAAAAEOil0PeWmcDSO+Dc8kDEdi3x3FiDDXZD1p/ogWEn2wfgyvSSiuft8K+AW1I7MZBCOw==", null, false, "JTPW4E432HW3NINUZXHV7YR7T2N5GISU", false, "juan@salvo.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d9ffb18e-f9ad-4c60-9678-4526e371c0c3");
        }
    }
}
