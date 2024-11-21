using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senior_Project_Graphic_Design_Portfolio.Migrations
{
    public partial class AddingPathImage5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ThreeDProjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "DigitalDesignProjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "BrandingProjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ThreeDProjects");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "DigitalDesignProjects");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "BrandingProjects");
        }
    }
}
