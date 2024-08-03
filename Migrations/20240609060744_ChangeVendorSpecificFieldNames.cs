using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVendorSpecificFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtoId",
                schema: "Transaction",
                table: "StoreOrder",
                newName: "DeliveryId");

            migrationBuilder.RenameColumn(
                name: "UrwayTransactionId",
                schema: "Transaction",
                table: "CustomerOrder",
                newName: "PaymentTransactionId");

            migrationBuilder.RenameColumn(
                name: "UrwayTrackId",
                schema: "Transaction",
                table: "CustomerOrder",
                newName: "PaymentTrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                schema: "Transaction",
                table: "StoreOrder",
                newName: "OtoId");

            migrationBuilder.RenameColumn(
                name: "PaymentTransactionId",
                schema: "Transaction",
                table: "CustomerOrder",
                newName: "UrwayTransactionId");

            migrationBuilder.RenameColumn(
                name: "PaymentTrackId",
                schema: "Transaction",
                table: "CustomerOrder",
                newName: "UrwayTrackId");
        }
    }
}
