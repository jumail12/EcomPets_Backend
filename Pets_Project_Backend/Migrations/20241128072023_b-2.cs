using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pets_Project_Backend.Migrations
{
    /// <inheritdoc />
    public partial class b2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "UserEmail", "UserName", "isBlocked" },
                values: new object[] { 1, "$2a$11$8C4wtW4z/9cE0KXpSdCoX.XXLoA9fjqERvnMl7OlpFCeMh2KZSh2C", "Admin", "jumailjumi2003@gmail.com", "Jumail", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
