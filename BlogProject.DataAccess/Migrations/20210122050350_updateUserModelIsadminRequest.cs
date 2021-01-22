using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class updateUserModelIsadminRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "UserModel",
                newName: "IsAdminRequest");

            migrationBuilder.AddColumn<int>(
                name: "IsAdminApproved",
                table: "UserModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdminApproved",
                table: "UserModel");

            migrationBuilder.RenameColumn(
                name: "IsAdminRequest",
                table: "UserModel",
                newName: "IsAdmin");
        }
    }
}
