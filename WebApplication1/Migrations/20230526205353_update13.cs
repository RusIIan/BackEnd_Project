using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartFullName",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "DownDescription",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "DownTitle",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "UpTitle",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Abouts",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "sliderImg",
                table: "Abouts",
                newName: "VideoLink");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Abouts",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Abouts",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "VideoLink",
                table: "Abouts",
                newName: "sliderImg");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Abouts",
                newName: "photo");

            migrationBuilder.AddColumn<string>(
                name: "CartFullName",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DownDescription",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DownTitle",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpTitle",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
