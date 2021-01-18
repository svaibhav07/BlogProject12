using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject12.DataAccess.Migrations
{
    public partial class addspfortag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetTagModels 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.TagModel 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetTagModel 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.TagModel  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateTagModel
	                                @Id int,
	                                @TagName varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.TagModel
                                     SET  TagName = @TagName
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteTagModel
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.TagModel
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateTagModel
                                   @TagName varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.TagModel(TagName)
                                    VALUES (@TagName)
                                   END");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetTags");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetTag");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateTag");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteTag");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateTag");

        }
    }
}
