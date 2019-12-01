using Microsoft.EntityFrameworkCore.Migrations;

namespace SurvivalPrep.Migrations.UsersRolesDBMigrations
{
    public partial class AddImageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Items");
        }
    }
}
