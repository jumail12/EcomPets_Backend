using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pets_Project_Backend.Migrations
{
    /// <inheritdoc />
    public partial class List5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhishList_Products_productId",
                table: "WhishList");

            migrationBuilder.AddForeignKey(
                name: "FK_WhishList_Products_productId",
                table: "WhishList",
                column: "productId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhishList_Products_productId",
                table: "WhishList");

            migrationBuilder.AddForeignKey(
                name: "FK_WhishList_Products_productId",
                table: "WhishList",
                column: "productId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
