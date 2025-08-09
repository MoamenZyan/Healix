using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Clinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fees",
                table: "Clinic",
                newName: "Hotline");

            migrationBuilder.AddColumn<string>(
                name: "PracticeLicenseUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clinic",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticeLicenseUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clinic");

            migrationBuilder.RenameColumn(
                name: "Hotline",
                table: "Clinic",
                newName: "Fees");
        }
    }
}
