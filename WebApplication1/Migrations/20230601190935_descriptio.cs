using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class descriptio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aboutreturnrequest",
                table: "ProductStylePages");

            migrationBuilder.DropColumn(
                name: "Guarantee",
                table: "ProductStylePages");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "ProductStylePages");

            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "ProductStylePages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "ProductStylePages");

            migrationBuilder.AddColumn<string>(
                name: "Aboutreturnrequest",
                table: "ProductStylePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Guarantee",
                table: "ProductStylePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping",
                table: "ProductStylePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
