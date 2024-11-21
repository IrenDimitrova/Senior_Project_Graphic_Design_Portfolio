using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senior_Project_Graphic_Design_Portfolio.Migrations
{
    public partial class AddProjectCommentsAndRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BrandingProjectProjectID = table.Column<int>(type: "int", nullable: true),
                    DigitalDesignProjectProjectID = table.Column<int>(type: "int", nullable: true),
                    PrintProjectProjectID = table.Column<int>(type: "int", nullable: true),
                    ThreeDProjectProjectID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectComments_BrandingProjects_BrandingProjectProjectID",
                        column: x => x.BrandingProjectProjectID,
                        principalTable: "BrandingProjects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_ProjectComments_DigitalDesignProjects_DigitalDesignProjectPr~",
                        column: x => x.DigitalDesignProjectProjectID,
                        principalTable: "DigitalDesignProjects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_ProjectComments_PrintProjects_PrintProjectProjectID",
                        column: x => x.PrintProjectProjectID,
                        principalTable: "PrintProjects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_ProjectComments_ThreeDProjects_ThreeDProjectProjectID",
                        column: x => x.ThreeDProjectProjectID,
                        principalTable: "ThreeDProjects",
                        principalColumn: "ProjectID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_BrandingProjectProjectID",
                table: "ProjectComments",
                column: "BrandingProjectProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_DigitalDesignProjectProjectID",
                table: "ProjectComments",
                column: "DigitalDesignProjectProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_PrintProjectProjectID",
                table: "ProjectComments",
                column: "PrintProjectProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_ThreeDProjectProjectID",
                table: "ProjectComments",
                column: "ThreeDProjectProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_UserId",
                table: "ProjectComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRatings_UserId",
                table: "ProjectRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectComments");

            migrationBuilder.DropTable(
                name: "ProjectRatings");
        }
    }
}
