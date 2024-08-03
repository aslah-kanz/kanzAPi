using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttributeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attribute_NameEn",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "Unit3En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit2En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit1En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupEn",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_GroupEn_NameEn_Unit1En_Unit2En_Unit3En",
                schema: "Product",
                table: "Attribute",
                columns: new[] { "GroupEn", "NameEn", "Unit1En", "Unit2En", "Unit3En" },
                unique: true,
                filter: "[Unit1En] IS NOT NULL AND [Unit2En] IS NOT NULL AND [Unit3En] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attribute_GroupEn_NameEn_Unit1En_Unit2En_Unit3En",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "Unit3En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit2En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit1En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupEn",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_NameEn",
                schema: "Product",
                table: "Attribute",
                column: "NameEn",
                unique: true);
        }
    }
}
