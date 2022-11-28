using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PublicIdAndUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Games",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Games",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Games",
                newName: "Image");
        }
    }
}
