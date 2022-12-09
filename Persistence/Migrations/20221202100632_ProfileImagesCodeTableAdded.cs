using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ProfileImagesCodeTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileImage",
                table: "ProfileImage");

            migrationBuilder.RenameTable(
                name: "ProfileImage",
                newName: "ProfileImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileImages",
                table: "ProfileImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfileImages_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "ProfileImages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfileImages_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileImages",
                table: "ProfileImages");

            migrationBuilder.RenameTable(
                name: "ProfileImages",
                newName: "ProfileImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileImage",
                table: "ProfileImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "ProfileImage",
                principalColumn: "Id");
        }
    }
}
