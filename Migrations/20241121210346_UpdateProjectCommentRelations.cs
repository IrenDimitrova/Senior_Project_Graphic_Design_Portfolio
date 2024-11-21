using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senior_Project_Graphic_Design_Portfolio.Migrations
{
    public partial class UpdateProjectCommentRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_BrandingProjects_BrandingProjectProjectID",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectProjectID",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_PrintProjects_PrintProjectProjectID",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_ThreeDProjects_ThreeDProjectProjectID",
                table: "ProjectComments");

            migrationBuilder.RenameColumn(
                name: "ThreeDProjectProjectID",
                table: "ProjectComments",
                newName: "ThreeDProjectId");

            migrationBuilder.RenameColumn(
                name: "PrintProjectProjectID",
                table: "ProjectComments",
                newName: "PrintProjectId");

            migrationBuilder.RenameColumn(
                name: "DigitalProjectProjectID",
                table: "ProjectComments",
                newName: "DigitalProjectId");

            migrationBuilder.RenameColumn(
                name: "BrandingProjectProjectID",
                table: "ProjectComments",
                newName: "BrandingProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_ThreeDProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_ThreeDProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_PrintProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_PrintProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_DigitalProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_DigitalProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_BrandingProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_BrandingProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectType",
                table: "ProjectComments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_BrandingProjects_BrandingProjectId",
                table: "ProjectComments",
                column: "BrandingProjectId",
                principalTable: "BrandingProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectId",
                table: "ProjectComments",
                column: "DigitalProjectId",
                principalTable: "DigitalDesignProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_PrintProjects_PrintProjectId",
                table: "ProjectComments",
                column: "PrintProjectId",
                principalTable: "PrintProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_ThreeDProjects_ThreeDProjectId",
                table: "ProjectComments",
                column: "ThreeDProjectId",
                principalTable: "ThreeDProjects",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_BrandingProjects_BrandingProjectId",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectId",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_PrintProjects_PrintProjectId",
                table: "ProjectComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_ThreeDProjects_ThreeDProjectId",
                table: "ProjectComments");

            migrationBuilder.RenameColumn(
                name: "ThreeDProjectId",
                table: "ProjectComments",
                newName: "ThreeDProjectProjectID");

            migrationBuilder.RenameColumn(
                name: "PrintProjectId",
                table: "ProjectComments",
                newName: "PrintProjectProjectID");

            migrationBuilder.RenameColumn(
                name: "DigitalProjectId",
                table: "ProjectComments",
                newName: "DigitalProjectProjectID");

            migrationBuilder.RenameColumn(
                name: "BrandingProjectId",
                table: "ProjectComments",
                newName: "BrandingProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_ThreeDProjectId",
                table: "ProjectComments",
                newName: "IX_ProjectComments_ThreeDProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_PrintProjectId",
                table: "ProjectComments",
                newName: "IX_ProjectComments_PrintProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_DigitalProjectId",
                table: "ProjectComments",
                newName: "IX_ProjectComments_DigitalProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_BrandingProjectId",
                table: "ProjectComments",
                newName: "IX_ProjectComments_BrandingProjectProjectID");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectType",
                table: "ProjectComments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_BrandingProjects_BrandingProjectProjectID",
                table: "ProjectComments",
                column: "BrandingProjectProjectID",
                principalTable: "BrandingProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectProjectID",
                table: "ProjectComments",
                column: "DigitalProjectProjectID",
                principalTable: "DigitalDesignProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_PrintProjects_PrintProjectProjectID",
                table: "ProjectComments",
                column: "PrintProjectProjectID",
                principalTable: "PrintProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_ThreeDProjects_ThreeDProjectProjectID",
                table: "ProjectComments",
                column: "ThreeDProjectProjectID",
                principalTable: "ThreeDProjects",
                principalColumn: "ProjectID");
        }
    }
}
