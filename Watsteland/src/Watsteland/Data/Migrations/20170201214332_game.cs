using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Watsteland.Data.Migrations
{
    public partial class game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_GameRules_GameRulesId",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_GameRulesId",
                table: "Rules");

            migrationBuilder.RenameColumn(
                name: "GameRulesId",
                table: "Rules",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "GameRulesId",
                table: "GameRules",
                newName: "GameId");

            migrationBuilder.AddColumn<string>(
                name: "GameDescription",
                table: "GameRules",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameDescription",
                table: "GameRules");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Rules",
                newName: "GameRulesId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "GameRules",
                newName: "GameRulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_GameRulesId",
                table: "Rules",
                column: "GameRulesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_GameRules_GameRulesId",
                table: "Rules",
                column: "GameRulesId",
                principalTable: "GameRules",
                principalColumn: "GameRulesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
