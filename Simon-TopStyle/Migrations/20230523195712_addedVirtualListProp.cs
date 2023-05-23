using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simon_TopStyle.Migrations
{
    /// <inheritdoc />
    public partial class addedVirtualListProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryCategortyId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products",
                column: "CategoryCategortyId",
                principalTable: "Categories",
                principalColumn: "CategortyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryCategortyId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCategortyId",
                table: "Products",
                column: "CategoryCategortyId",
                principalTable: "Categories",
                principalColumn: "CategortyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
