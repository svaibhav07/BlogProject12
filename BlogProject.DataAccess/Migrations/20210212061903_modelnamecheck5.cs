using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class modelnamecheck5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "tblPersons",
                newName: "stLastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stLastName",
                table: "tblPersons",
                newName: "LastName");
        }
    }
}
