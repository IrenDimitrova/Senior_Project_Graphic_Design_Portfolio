using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senior_Project_Graphic_Design_Portfolio.Migrations
{
    public partial class AddingPathImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "PrintProjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "PrintProjects");
        }
    }
}
