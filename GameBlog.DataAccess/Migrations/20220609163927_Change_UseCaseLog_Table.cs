using Microsoft.EntityFrameworkCore.Migrations;

namespace GameBlog.DataAccess.Migrations
{
    public partial class Change_UseCaseLog_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Actor",
                table: "UseCaseLogs",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "UseCaseName",
                table: "UseCaseLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuthorized",
                table: "UseCaseLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UseCaseLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_CreatedAt",
                table: "UseCaseLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_UseCaseName",
                table: "UseCaseLogs",
                column: "UseCaseName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UseCaseLogs_CreatedAt",
                table: "UseCaseLogs");

            migrationBuilder.DropIndex(
                name: "IX_UseCaseLogs_UseCaseName",
                table: "UseCaseLogs");

            migrationBuilder.DropColumn(
                name: "IsAuthorized",
                table: "UseCaseLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UseCaseLogs");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UseCaseLogs",
                newName: "Actor");

            migrationBuilder.AlterColumn<string>(
                name: "UseCaseName",
                table: "UseCaseLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
