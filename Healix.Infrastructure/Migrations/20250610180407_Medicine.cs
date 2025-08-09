using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Medicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Medicines");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Medicines",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Medicines",
                type: "text",
                nullable: true);
        }
    }
}
