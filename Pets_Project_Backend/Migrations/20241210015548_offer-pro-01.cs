using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pets_Project_Backend.Migrations
{
    /// <inheritdoc />
    public partial class offerpro01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrize",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPrize",
                table: "Products");
        }
    }
}
