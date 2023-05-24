using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Percent",
                table: "Sliders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Sliders");
        }
    }
}
