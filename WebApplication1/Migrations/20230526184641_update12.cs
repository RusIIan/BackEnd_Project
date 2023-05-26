using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SliderImg",
                table: "Abouts",
                newName: "sliderImg");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Abouts",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "CartImg",
                table: "Abouts",
                newName: "image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sliderImg",
                table: "Abouts",
                newName: "SliderImg");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Abouts",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Abouts",
                newName: "CartImg");
        }
    }
}
