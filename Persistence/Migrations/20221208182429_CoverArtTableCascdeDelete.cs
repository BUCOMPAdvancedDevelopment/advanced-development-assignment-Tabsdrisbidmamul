using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class CoverArtTableCascdeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
