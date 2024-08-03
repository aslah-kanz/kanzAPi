using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class StoreOrderPurchaseQuoteStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseQuoteStatus",
                schema: "Transaction",
                table: "StoreOrder",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseQuoteStatus",
                schema: "Transaction",
                table: "StoreOrder");
        }
    }
}
