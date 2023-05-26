using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bestseller",
                table: "HomeCategories");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "HomeCategories");

            migrationBuilder.RenameColumn(
                name: "Latest",
                table: "HomeCategories",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "HomeCategories",
                newName: "Latest");

            migrationBuilder.AddColumn<string>(
                name: "Bestseller",
                table: "HomeCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Featured",
                table: "HomeCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
