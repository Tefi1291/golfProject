using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolfAPI.Migrations
{
    public partial class seedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Firstname", "Guid", "Lastname", "Password", "Role", "Username" },
                values: new object[] { 1, "Steven Patrick", new Guid("00000000-0000-0000-0000-000000000000"), "Morrissey", "admin123", 0, "morrissey" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
