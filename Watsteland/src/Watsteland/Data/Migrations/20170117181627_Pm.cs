using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Watsteland.Data.Migrations
{
    public partial class Pm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivateMessage",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromUserId = table.Column<string>(nullable: true),
                    FromUserName = table.Column<string>(nullable: true),
                    MessageDescription = table.Column<string>(nullable: true),
                    MessageTitle = table.Column<string>(nullable: true),
                    ToUserId = table.Column<string>(nullable: true),
                    ToUserName = table.Column<string>(nullable: true),
                    Unread = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateMessage", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateMessage");
        }
    }
}
