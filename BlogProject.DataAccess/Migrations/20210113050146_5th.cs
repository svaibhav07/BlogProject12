using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class _5th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedBlogs = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogModel_TagModel_TagId",
                        column: x => x.TagId,
                        principalTable: "TagModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogModel_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogModel_TagId",
                table: "BlogModel",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogModel_UserId",
                table: "BlogModel",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogModel");

            migrationBuilder.DropTable(
                name: "TagModel");
        }
    }
}
