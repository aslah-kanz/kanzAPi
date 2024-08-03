using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Product",
                table: "Brand",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(160)",
                oldMaxLength: 160);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Slug",
                schema: "Product",
                table: "Product",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Slug",
                schema: "Product",
                table: "Category",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_NameEn",
                schema: "Product",
                table: "Brand",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Slug",
                schema: "Product",
                table: "Brand",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Slug",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Category_Slug",
                schema: "Product",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Brand_NameEn",
                schema: "Product",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_Slug",
                schema: "Product",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "Product",
                table: "Brand",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
