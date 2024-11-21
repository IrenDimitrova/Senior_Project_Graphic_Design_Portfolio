using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senior_Project_Graphic_Design_Portfolio.Migrations
{
    public partial class AddCommentSyste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalDesignProjectPr~",
                table: "ProjectComments");

            migrationBuilder.RenameColumn(
                name: "DigitalDesignProjectProjectID",
                table: "ProjectComments",
                newName: "DigitalProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_DigitalDesignProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_DigitalProjectProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectProjectID",
                table: "ProjectComments",
                column: "DigitalProjectProjectID",
                principalTable: "DigitalDesignProjects",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalProjectProjectID",
                table: "ProjectComments");

            migrationBuilder.RenameColumn(
                name: "DigitalProjectProjectID",
                table: "ProjectComments",
                newName: "DigitalDesignProjectProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComments_DigitalProjectProjectID",
                table: "ProjectComments",
                newName: "IX_ProjectComments_DigitalDesignProjectProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_DigitalDesignProjects_DigitalDesignProjectPr~",
                table: "ProjectComments",
                column: "DigitalDesignProjectProjectID",
                principalTable: "DigitalDesignProjects",
                principalColumn: "ProjectID");
        }
    }
}
