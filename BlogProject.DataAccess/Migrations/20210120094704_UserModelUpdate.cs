using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class UserModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordConfirm",
                table: "UserModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordConfirm",
                table: "UserModel");
        }
    }
}
