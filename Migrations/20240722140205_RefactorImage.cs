using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactorImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Image_Name",
                schema: "Common",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "Common",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                schema: "Common",
                table: "Image",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_Group_Name",
                schema: "Common",
                table: "Image",
                columns: new[] { "Group", "Name" },
                unique: true,
                filter: "[Group] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Image_Group_Name",
                schema: "Common",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Group",
                schema: "Common",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "Common",
                table: "Image",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Name",
                schema: "Common",
                table: "Image",
                column: "Name",
                unique: true);
        }
    }
}
