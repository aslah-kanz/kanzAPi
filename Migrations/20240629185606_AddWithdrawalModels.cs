using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanzApi.Migrations
{
    /// <inheritdoc />
    public partial class AddWithdrawalModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                schema: "Transaction",
                table: "Withdraw",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankHolder",
                schema: "Transaction",
                table: "Withdraw",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                schema: "Transaction",
                table: "Withdraw",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WithdrawMethod",
                schema: "Transaction",
                table: "Withdraw",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                schema: "Transaction",
                table: "Withdraw");

            migrationBuilder.DropColumn(
                name: "BankHolder",
                schema: "Transaction",
                table: "Withdraw");

            migrationBuilder.DropColumn(
                name: "BankName",
                schema: "Transaction",
                table: "Withdraw");

            migrationBuilder.DropColumn(
                name: "WithdrawMethod",
                schema: "Transaction",
                table: "Withdraw");
        }
    }
}
