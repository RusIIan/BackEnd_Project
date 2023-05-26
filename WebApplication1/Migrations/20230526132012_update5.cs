using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "HomeProducts");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "HomeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "HomeProducts",
                newName: "IX_HomeProducts_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "HoverImage",
                table: "HomeProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeProducts",
                table: "HomeProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeCategories",
                table: "HomeCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProducts_HomeCategories_CategoryId",
                table: "HomeProducts",
                column: "CategoryId",
                principalTable: "HomeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProducts_HomeCategories_CategoryId",
                table: "HomeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeProducts",
                table: "HomeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeCategories",
                table: "HomeCategories");

            migrationBuilder.DropColumn(
                name: "HoverImage",
                table: "HomeProducts");

            migrationBuilder.RenameTable(
                name: "HomeProducts",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "HomeCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_HomeProducts_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
