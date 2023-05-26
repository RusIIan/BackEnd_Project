using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoverImage",
                table: "HomeProducts");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "HomeProducts",
                newName: "Star");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Star",
                table: "HomeProducts",
                newName: "Start");

            migrationBuilder.AddColumn<string>(
                name: "HoverImage",
                table: "HomeProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
