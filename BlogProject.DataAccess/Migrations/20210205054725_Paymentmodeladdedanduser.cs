using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class Paymentmodeladdedanduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PaymentInitiateModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInitiateModels_UserId",
                table: "PaymentInitiateModels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInitiateModels_UserModel_UserId",
                table: "PaymentInitiateModels",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInitiateModels_UserModel_UserId",
                table: "PaymentInitiateModels");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInitiateModels_UserId",
                table: "PaymentInitiateModels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentInitiateModels");
        }
    }
}
