using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class CoverArtCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_CoverArt_CoverArtId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CoverArtId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CoverArtId",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "CoverArt",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBoxArt",
                table: "CoverArt",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CoverArt_GameId",
                table: "CoverArt",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverArt_Games_GameId",
                table: "CoverArt");

            migrationBuilder.DropIndex(
                name: "IX_CoverArt_GameId",
                table: "CoverArt");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "CoverArt");

            migrationBuilder.DropColumn(
                name: "IsBoxArt",
                table: "CoverArt");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverArtId",
                table: "Games",
                type: "uuid",
                nullable: true);

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
    }
}
