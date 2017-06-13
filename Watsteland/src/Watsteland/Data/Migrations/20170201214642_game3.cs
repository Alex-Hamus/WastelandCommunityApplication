using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Watsteland.Data.Migrations
{
    public partial class game3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRules",
                table: "GameRules");

            migrationBuilder.RenameTable(
                name: "GameRules",
                newName: "Game");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "GameRules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRules",
                table: "GameRules",
                column: "GameId");
        }
    }
}
