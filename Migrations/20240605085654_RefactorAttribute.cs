using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Brand_BrandId",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Category_CategoryId",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropTable(
                name: "ProductPropertyItem",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductProperty",
                schema: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_BrandId",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_CategoryId",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.RenameColumn(
                name: "Options",
                schema: "Product",
                table: "Attribute",
                newName: "GroupEn");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "Product",
                table: "Attribute",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                schema: "Product",
                table: "Attribute",
                newName: "GroupOrder");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupAr",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit1Ar",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit1En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit2Ar",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit2En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit3Ar",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit3En",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldsEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldsAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    Value1En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value1Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value2En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value2Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value3En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value3Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Product",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Product",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_NameEn",
                schema: "Product",
                table: "Attribute",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeId",
                schema: "Product",
                table: "ProductAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ImageId",
                schema: "Product",
                table: "ProductAttribute",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId_AttributeId",
                schema: "Product",
                table: "ProductAttribute",
                columns: new[] { "ProductId", "AttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_PropertyId",
                schema: "Product",
                table: "ProductAttribute",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_NameEn",
                schema: "Product",
                table: "Property",
                column: "NameEn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttribute",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Property",
                schema: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_NameEn",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "GroupAr",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "NameAr",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "NameEn",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit1Ar",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit1En",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit2Ar",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit2En",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit3Ar",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "Unit3En",
                schema: "Product",
                table: "Attribute");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                schema: "Product",
                table: "Attribute",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "GroupOrder",
                schema: "Product",
                table: "Attribute",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "GroupEn",
                schema: "Product",
                table: "Attribute",
                newName: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Product",
                table: "Attribute",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductProperty",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Fields = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProperty_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPropertyItem",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    SaveAsAttribute = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPropertyItem_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPropertyItem_ProductProperty_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Product",
                        principalTable: "ProductProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_BrandId",
                schema: "Product",
                table: "Attribute",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CategoryId",
                schema: "Product",
                table: "Attribute",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperty_ProductId_SortOrder",
                schema: "Product",
                table: "ProductProperty",
                columns: new[] { "ProductId", "SortOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyItem_ImageId",
                schema: "Product",
                table: "ProductPropertyItem",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyItem_PropertyId",
                schema: "Product",
                table: "ProductPropertyItem",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Brand_BrandId",
                schema: "Product",
                table: "Attribute",
                column: "BrandId",
                principalSchema: "Product",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Category_CategoryId",
                schema: "Product",
                table: "Attribute",
                column: "CategoryId",
                principalSchema: "Product",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
