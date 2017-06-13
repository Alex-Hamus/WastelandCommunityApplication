using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Watsteland.Data.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Thread",
                table: "Thread");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateMessage",
                table: "PrivateMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forum",
                table: "Forum");

            migrationBuilder.RenameTable(
                name: "Thread",
                newName: "Threads");

            migrationBuilder.RenameTable(
                name: "PrivateMessage",
                newName: "PrivateMessages");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Forum",
                newName: "Forums");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "ThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateMessages",
                table: "PrivateMessages",
                column: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forums",
                table: "Forums",
                column: "ForumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateMessages",
                table: "PrivateMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forums",
                table: "Forums");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "Thread");

            migrationBuilder.RenameTable(
                name: "PrivateMessages",
                newName: "PrivateMessage");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Forums",
                newName: "Forum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Thread",
                table: "Thread",
                column: "ThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateMessage",
                table: "PrivateMessage",
                column: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forum",
                table: "Forum",
                column: "ForumId");
        }
    }
}
