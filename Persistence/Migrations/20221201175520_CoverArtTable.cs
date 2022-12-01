using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class CoverArtTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverArtId",
                table: "Games",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoverArt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverArt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_CoverArtId",
                table: "Games",
                column: "CoverArtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_CoverArt_CoverArtId",
                table: "Games",
                column: "CoverArtId",
                principalTable: "CoverArt",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_CoverArt_CoverArtId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "CoverArt");

            migrationBuilder.DropIndex(
                name: "IX_Games_CoverArtId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CoverArtId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Games",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Games",
                type: "text",
                nullable: true);
        }
    }
}
