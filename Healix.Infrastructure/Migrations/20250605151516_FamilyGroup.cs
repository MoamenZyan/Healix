using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FamilyGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyGroupId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FamilyGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FamilyGroupId",
                table: "Users",
                column: "FamilyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FamilyGroups_FamilyGroupId",
                table: "Users",
                column: "FamilyGroupId",
                principalTable: "FamilyGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FamilyGroups_FamilyGroupId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "FamilyGroups");

            migrationBuilder.DropIndex(
                name: "IX_Users_FamilyGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FamilyGroupId",
                table: "Users");
        }
    }
}
