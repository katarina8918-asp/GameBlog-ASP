using Microsoft.EntityFrameworkCore.Migrations;

namespace GameBlog.DataAccess.Migrations
{
    public partial class Change_UseCaseLog_Table_Column_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UseCaseLogs",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UseCaseLogs",
                newName: "Username");
        }
    }
}
