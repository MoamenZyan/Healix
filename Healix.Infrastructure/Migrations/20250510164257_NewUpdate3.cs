using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatBots_Users_ApplicationUserId",
                table: "ChatBots");

            migrationBuilder.DropIndex(
                name: "IX_ChatBots_ApplicationUserId",
                table: "ChatBots");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ChatBots");

            migrationBuilder.RenameColumn(
                name: "IsAi",
                table: "ChatMessages",
                newName: "IsUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ChatBots",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ChatMessageFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    ChatMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessageFiles_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBots_UserId",
                table: "ChatBots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageFiles_ChatMessageId",
                table: "ChatMessageFiles",
                column: "ChatMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBots_Users_UserId",
                table: "ChatBots",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatBots_Users_UserId",
                table: "ChatBots");

            migrationBuilder.DropTable(
                name: "ChatMessageFiles");

            migrationBuilder.DropIndex(
                name: "IX_ChatBots_UserId",
                table: "ChatBots");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatBots");

            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "ChatMessages",
                newName: "IsAi");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ChatBots",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatBots_ApplicationUserId",
                table: "ChatBots",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBots_Users_ApplicationUserId",
                table: "ChatBots",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
