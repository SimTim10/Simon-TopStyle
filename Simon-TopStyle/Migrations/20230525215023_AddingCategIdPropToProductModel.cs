using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simon_TopStyle.Migrations
{
    /// <inheritdoc />
    public partial class AddingCategIdPropToProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryCategortyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryCategortyId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategortyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryCategortyId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCategortyId",
                table: "Products",
                column: "CategoryCategortyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products",
                column: "CategoryCategortyId",
                principalTable: "Categories",
                principalColumn: "CategortyId");
        }
    }
}
