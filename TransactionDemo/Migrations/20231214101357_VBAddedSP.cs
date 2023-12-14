using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionDemo.Migrations
{
    /// <inheritdoc />
    public partial class VBAddedSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[VBProc]	
                                AS
                                Begin
	                                SELECT * from dbo.ProductInfos
                                End");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
