using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class B2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Users",
                newName: "PhotoUrlAuthorization");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "PatientRecords",
                newName: "UrlAuthorization");

            migrationBuilder.AddColumn<DateTime>(
                name: "PhotoUrlExpiration",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FileUrlExpiration",
                table: "PatientRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrlExpiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileUrlExpiration",
                table: "PatientRecords");

            migrationBuilder.RenameColumn(
                name: "PhotoUrlAuthorization",
                table: "Users",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "UrlAuthorization",
                table: "PatientRecords",
                newName: "FileUrl");
        }
    }
}
