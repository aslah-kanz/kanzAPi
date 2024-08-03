using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.EnsureSchema(
                name: "Transaction");

            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.EnsureSchema(
                name: "Logging");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaqGroup",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ShowAtHomePage = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalRequestLog",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalRequestLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalResponseLog",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalResponseLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobField",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestLog",
                schema: "Logging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethod",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProviderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryCompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryEstimateTime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriber",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suggestion",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faq",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FaqGroupId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faq_FaqGroup_FaqGroupId",
                        column: x => x.FaqGroupId,
                        principalSchema: "Common",
                        principalTable: "FaqGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banner_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    ReadCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    BwImageId = table.Column<long>(type: "bigint", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    ShowAtHomePage = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_Image_BwImageId",
                        column: x => x.BwImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brand_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Catalogue",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    ReadCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogue_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Common",
                        principalTable: "Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Catalogue_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    ShowAtHomePage = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Product",
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Category_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PhoneCode = table.Column<int>(type: "int", nullable: false),
                    PhoneStartNumber = table.Column<int>(type: "int", nullable: false),
                    PhoneMinLength = table.Column<int>(type: "int", nullable: false),
                    PhoneMaxLength = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Language_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    ImageArId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Image_ImageArId",
                        column: x => x.ImageArId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Principal",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AcceptNewsLetter = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principal_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WebPage",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShowAtHomePage = table.Column<bool>(type: "bit", nullable: false),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WebsiteProfile",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    FaviconId = table.Column<long>(type: "bigint", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Linkedin = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteProfile_Image_FaviconId",
                        column: x => x.FaviconId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WebsiteProfile_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Requirement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    JobFieldId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_JobField_JobFieldId",
                        column: x => x.JobFieldId,
                        principalSchema: "Common",
                        principalTable: "JobField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePrivilege",
                schema: "Account",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PrivilegeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivilege", x => new { x.PrivilegeId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePrivilege_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalSchema: "Account",
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePrivilege_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Account",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Product",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attribute_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Product",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BrandCategory",
                schema: "Product",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandCategory", x => new { x.BrandId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BrandCategory_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Product",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Product",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalDetail",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    CompanyNumber = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    CompanyNameEn = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    CompanyNameAr = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalDetail_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Common",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageArgs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReferenceId = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    ReadAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OneTimeToken",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneTimeToken_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Otp",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Otp_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalAddress",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(13,8)", nullable: false),
                    DefaultAddress = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalAddress_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalBank",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    City = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalBank_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Common",
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrincipalBank_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Common",
                        principalTable: "Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrincipalBank_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalRole",
                schema: "Account",
                columns: table => new
                {
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalRole", x => new { x.PrincipalId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PrincipalRole_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrincipalRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Account",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalWallet",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalWallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalWallet_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccessTokenId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WarehouseId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(12,8)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(13,8)", nullable: false),
                    SaleItemCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Withdraw",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdraw_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Withdraw_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicant",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicant_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Common",
                        principalTable: "Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplicant_Job_JobId",
                        column: x => x.JobId,
                        principalSchema: "Common",
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMember",
                schema: "Account",
                columns: table => new
                {
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMember", x => new { x.PrincipalDetailId, x.PrincipalId });
                    table.ForeignKey(
                        name: "FK_CompanyMember_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyMember_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalDetailItem",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalDetailItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalDetailItem_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mpn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FamilyCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    IconId = table.Column<long>(type: "bigint", nullable: true),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    MetaKeyword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sellable = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Product",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Image_IconId",
                        column: x => x.IconId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrincipalStore",
                schema: "Account",
                columns: table => new
                {
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalStore", x => new { x.PrincipalId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_PrincipalStore_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrincipalStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalDepartment",
                schema: "Account",
                columns: table => new
                {
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalDepartment", x => new { x.DepartmentId, x.PrincipalId });
                    table.ForeignKey(
                        name: "FK_PrincipalDepartment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Account",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrincipalDepartment_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrwayTrackId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UrwayTransactionId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PurchaseQuoteStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HiglightedProductId = table.Column<int>(type: "int", nullable: false),
                    DeliveryOptionId = table.Column<int>(type: "int", nullable: true),
                    EstimatedDeliveryCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeliveryOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOrder_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "Transaction",
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrder_PrincipalAddress_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Account",
                        principalTable: "PrincipalAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrder_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerOrder_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrder_Product_HiglightedProductId",
                        column: x => x.HiglightedProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inquiry",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiry_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inquiry_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Product",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProperty",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
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
                name: "SaleItem",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VendorSku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ReservedStock = table.Column<int>(type: "int", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItem_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishList_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderItem",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItem_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalSchema: "Transaction",
                        principalTable: "CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingMethodId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipping_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalSchema: "Transaction",
                        principalTable: "CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipping_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalSchema: "Transaction",
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreOrder",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OtoId = table.Column<int>(type: "int", nullable: true),
                    CustomerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: true),
                    DeliveryCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreOrder_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalSchema: "Transaction",
                        principalTable: "CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreOrder_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPropertyItem",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SaveAsAttribute = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "CartSaleItem",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    SaleItemId = table.Column<long>(type: "bigint", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    VendorSku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSaleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartSaleItem_Cart_CartId",
                        column: x => x.CartId,
                        principalSchema: "Transaction",
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartSaleItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartSaleItem_SaleItem_SaleItemId",
                        column: x => x.SaleItemId,
                        principalSchema: "Product",
                        principalTable: "SaleItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartSaleItem_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseQuote",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VendorSku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerOrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    SaleItemId = table.Column<long>(type: "bigint", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    ConfirmedQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalRequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlatformCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseQuote_CustomerOrderItem_CustomerOrderItemId",
                        column: x => x.CustomerOrderItemId,
                        principalSchema: "Transaction",
                        principalTable: "CustomerOrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseQuote_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalSchema: "Transaction",
                        principalTable: "CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseQuote_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseQuote_StoreOrder_StoreOrderId",
                        column: x => x.StoreOrderId,
                        principalSchema: "Transaction",
                        principalTable: "StoreOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseQuote_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exchange",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PurchaseQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdminComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VendorComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchange_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exchange_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchange_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exchange_PurchaseQuote_PurchaseQuoteId",
                        column: x => x.PurchaseQuoteId,
                        principalSchema: "Transaction",
                        principalTable: "PurchaseQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchange_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    PurchaseQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReview_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReview_PurchaseQuote_PurchaseQuoteId",
                        column: x => x.PurchaseQuoteId,
                        principalSchema: "Transaction",
                        principalTable: "PurchaseQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                schema: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PurchaseQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    PrincipalDetailId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdminComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VendorComment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refund_PrincipalDetail_PrincipalDetailId",
                        column: x => x.PrincipalDetailId,
                        principalSchema: "Account",
                        principalTable: "PrincipalDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Refund_Principal_PrincipalId",
                        column: x => x.PrincipalId,
                        principalSchema: "Account",
                        principalTable: "Principal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Refund_PurchaseQuote_PurchaseQuoteId",
                        column: x => x.PurchaseQuoteId,
                        principalSchema: "Transaction",
                        principalTable: "PurchaseQuote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Account",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeImage",
                schema: "Transaction",
                columns: table => new
                {
                    ExchangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeImage", x => new { x.ExchangeId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ExchangeImage_Exchange_ExchangeId",
                        column: x => x.ExchangeId,
                        principalSchema: "Transaction",
                        principalTable: "Exchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviewImage",
                schema: "Transaction",
                columns: table => new
                {
                    ProductReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviewImage", x => new { x.ImageId, x.ProductReviewId });
                    table.ForeignKey(
                        name: "FK_ProductReviewImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviewImage_ProductReview_ProductReviewId",
                        column: x => x.ProductReviewId,
                        principalSchema: "Transaction",
                        principalTable: "ProductReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundImage",
                schema: "Transaction",
                columns: table => new
                {
                    RefundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundImage", x => new { x.ImageId, x.RefundId });
                    table.ForeignKey(
                        name: "FK_RefundImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Common",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundImage_Refund_RefundId",
                        column: x => x.RefundId,
                        principalSchema: "Transaction",
                        principalTable: "Refund",
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
                name: "IX_Banner_ImageId",
                schema: "Common",
                table: "Banner",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_ImageId",
                schema: "Common",
                table: "Blog",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_BwImageId",
                schema: "Product",
                table: "Brand",
                column: "BwImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ImageId",
                schema: "Product",
                table: "Brand",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandCategory_CategoryId",
                schema: "Product",
                table: "BrandCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_PrincipalId_ProductId_Price",
                schema: "Transaction",
                table: "Cart",
                columns: new[] { "PrincipalId", "ProductId", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                schema: "Transaction",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartSaleItem_CartId",
                schema: "Transaction",
                table: "CartSaleItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartSaleItem_ProductId",
                schema: "Transaction",
                table: "CartSaleItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartSaleItem_SaleItemId",
                schema: "Transaction",
                table: "CartSaleItem",
                column: "SaleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartSaleItem_StoreId",
                schema: "Transaction",
                table: "CartSaleItem",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogue_DocumentId",
                schema: "Common",
                table: "Catalogue",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogue_ImageId",
                schema: "Common",
                table: "Catalogue",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ImageId",
                schema: "Product",
                table: "Category",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "Product",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_ImageId",
                schema: "Common",
                table: "Certificate",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMember_PrincipalId",
                schema: "Account",
                table: "CompanyMember",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ImageId",
                schema: "Common",
                table: "Country",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_PhoneCode",
                schema: "Common",
                table: "Country",
                column: "PhoneCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code",
                schema: "Common",
                table: "Currency",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_AddressId",
                schema: "Transaction",
                table: "CustomerOrder",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_HiglightedProductId",
                schema: "Transaction",
                table: "CustomerOrder",
                column: "HiglightedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_PaymentMethodId",
                schema: "Transaction",
                table: "CustomerOrder",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_PrincipalDetailId",
                schema: "Transaction",
                table: "CustomerOrder",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_PrincipalId",
                schema: "Transaction",
                table: "CustomerOrder",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItem_CustomerOrderId",
                schema: "Transaction",
                table: "CustomerOrderItem",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItem_ProductId",
                schema: "Transaction",
                table: "CustomerOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_PrincipalDetailId",
                schema: "Account",
                table: "Department",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_Name",
                schema: "Common",
                table: "Document",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_PrincipalDetailId",
                schema: "Transaction",
                table: "Exchange",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_PrincipalId",
                schema: "Transaction",
                table: "Exchange",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_ProductId",
                schema: "Transaction",
                table: "Exchange",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_PurchaseQuoteId",
                schema: "Transaction",
                table: "Exchange",
                column: "PurchaseQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_StoreId",
                schema: "Transaction",
                table: "Exchange",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeImage_ImageId",
                schema: "Transaction",
                table: "ExchangeImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Faq_FaqGroupId",
                schema: "Common",
                table: "Faq",
                column: "FaqGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Name",
                schema: "Common",
                table: "Image",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_PrincipalId",
                schema: "Transaction",
                table: "Inquiry",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_ProductId",
                schema: "Transaction",
                table: "Inquiry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobFieldId",
                schema: "Common",
                table: "Job",
                column: "JobFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicant_DocumentId",
                schema: "Common",
                table: "JobApplicant",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicant_JobId",
                schema: "Common",
                table: "JobApplicant",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_Code",
                schema: "Common",
                table: "Language",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Language_ImageId",
                schema: "Common",
                table: "Language",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ImageId",
                schema: "Account",
                table: "Notification",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_PrincipalId",
                schema: "Account",
                table: "Notification",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeToken_PrincipalId",
                schema: "Security",
                table: "OneTimeToken",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeToken_Token",
                schema: "Security",
                table: "OneTimeToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeToken_Type_PrincipalId",
                schema: "Security",
                table: "OneTimeToken",
                columns: new[] { "Type", "PrincipalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Code",
                schema: "Security",
                table: "Otp",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Otp_PrincipalId",
                schema: "Security",
                table: "Otp",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_ImageArId",
                schema: "Transaction",
                table: "PaymentMethod",
                column: "ImageArId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_ImageId",
                schema: "Transaction",
                table: "PaymentMethod",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_CountryCode_PhoneNumber",
                schema: "Account",
                table: "Principal",
                columns: new[] { "CountryCode", "PhoneNumber" },
                unique: true,
                filter: "[CountryCode] IS NOT NULL AND [PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_Email",
                schema: "Account",
                table: "Principal",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Principal_ImageId",
                schema: "Account",
                table: "Principal",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Principal_Username",
                schema: "Account",
                table: "Principal",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalAddress_PrincipalId_Name",
                schema: "Account",
                table: "PrincipalAddress",
                columns: new[] { "PrincipalId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalBank_CurrencyId",
                schema: "Account",
                table: "PrincipalBank",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalBank_DocumentId",
                schema: "Account",
                table: "PrincipalBank",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalBank_Iban",
                schema: "Account",
                table: "PrincipalBank",
                column: "Iban",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalBank_PrincipalId",
                schema: "Account",
                table: "PrincipalBank",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalDepartment_PrincipalId",
                schema: "Account",
                table: "PrincipalDepartment",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalDetail_CountryId",
                schema: "Account",
                table: "PrincipalDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalDetail_PrincipalId",
                schema: "Account",
                table: "PrincipalDetail",
                column: "PrincipalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalDetailItem_PrincipalDetailId",
                schema: "Account",
                table: "PrincipalDetailItem",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalRole_RoleId",
                schema: "Account",
                table: "PrincipalRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalStore_StoreId",
                schema: "Account",
                table: "PrincipalStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalWallet_PrincipalId",
                schema: "Account",
                table: "PrincipalWallet",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_Name",
                schema: "Account",
                table: "Privilege",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "Product",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IconId",
                schema: "Product",
                table: "Product",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                schema: "Product",
                table: "Product",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PrincipalDetailId",
                schema: "Product",
                table: "Product",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                schema: "Product",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ImageId",
                schema: "Product",
                table: "ProductImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId_ImageId",
                schema: "Product",
                table: "ProductImage",
                columns: new[] { "ProductId", "ImageId" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_PrincipalId",
                schema: "Transaction",
                table: "ProductReview",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                schema: "Transaction",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_PurchaseQuoteId",
                schema: "Transaction",
                table: "ProductReview",
                column: "PurchaseQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviewImage_ProductReviewId",
                schema: "Transaction",
                table: "ProductReviewImage",
                column: "ProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuote_CustomerOrderId",
                schema: "Transaction",
                table: "PurchaseQuote",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuote_CustomerOrderItemId",
                schema: "Transaction",
                table: "PurchaseQuote",
                column: "CustomerOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuote_ProductId",
                schema: "Transaction",
                table: "PurchaseQuote",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuote_StoreId",
                schema: "Transaction",
                table: "PurchaseQuote",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuote_StoreOrderId",
                schema: "Transaction",
                table: "PurchaseQuote",
                column: "StoreOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_PrincipalId",
                schema: "Security",
                table: "RefreshToken",
                column: "PrincipalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "Security",
                table: "RefreshToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PrincipalDetailId",
                schema: "Transaction",
                table: "Refund",
                column: "PrincipalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PrincipalId",
                schema: "Transaction",
                table: "Refund",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_ProductId",
                schema: "Transaction",
                table: "Refund",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PurchaseQuoteId",
                schema: "Transaction",
                table: "Refund",
                column: "PurchaseQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_StoreId",
                schema: "Transaction",
                table: "Refund",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundImage_RefundId",
                schema: "Transaction",
                table: "RefundImage",
                column: "RefundId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                schema: "Account",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivilege_RoleId",
                schema: "Account",
                table: "RolePrivilege",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_ProductId_StoreId",
                schema: "Product",
                table: "SaleItem",
                columns: new[] { "ProductId", "StoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_StoreId",
                schema: "Product",
                table: "SaleItem",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_CustomerOrderId",
                schema: "Transaction",
                table: "Shipping",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_ShippingMethodId",
                schema: "Transaction",
                table: "Shipping",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_PrincipalId",
                schema: "Account",
                table: "Store",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreOrder_CustomerOrderId",
                schema: "Transaction",
                table: "StoreOrder",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreOrder_InvoiceNumber",
                schema: "Transaction",
                table: "StoreOrder",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreOrder_StoreId",
                schema: "Transaction",
                table: "StoreOrder",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_ImageId",
                schema: "Common",
                table: "WebPage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_Slug",
                schema: "Common",
                table: "WebPage",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteProfile_FaviconId",
                schema: "Common",
                table: "WebsiteProfile",
                column: "FaviconId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteProfile_ImageId",
                schema: "Common",
                table: "WebsiteProfile",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_PrincipalId",
                schema: "Transaction",
                table: "WishList",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_ProductId_PrincipalId",
                schema: "Transaction",
                table: "WishList",
                columns: new[] { "ProductId", "PrincipalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Withdraw_ImageId",
                schema: "Transaction",
                table: "Withdraw",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraw_PrincipalId",
                schema: "Transaction",
                table: "Withdraw",
                column: "PrincipalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Banner",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Blog",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "BrandCategory",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "CartSaleItem",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Catalogue",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Certificate",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "CompanyMember",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "ErrorLog",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "ExchangeImage",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Faq",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Inquiry",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "InternalRequestLog",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "InternalResponseLog",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "JobApplicant",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "OneTimeToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Otp",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "PrincipalBank",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PrincipalDepartment",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PrincipalDetailItem",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PrincipalRole",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PrincipalStore",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PrincipalWallet",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductPropertyItem",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductReviewImage",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RefundImage",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "RequestLog",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "RolePrivilege",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Shipping",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Subscriber",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Suggestion",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "WebPage",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "WebsiteProfile",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "WishList",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Withdraw",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Cart",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "SaleItem",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Exchange",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "FaqGroup",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductProperty",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductReview",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Refund",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Privilege",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "ShippingMethod",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "JobField",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "PurchaseQuote",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "CustomerOrderItem",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "StoreOrder",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "CustomerOrder",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "Transaction");

            migrationBuilder.DropTable(
                name: "PrincipalAddress",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Principal",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "PrincipalDetail",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "Common");
        }
    }
}
