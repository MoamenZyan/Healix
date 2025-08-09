using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_Users_ApplicationUserId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecords_ApplicationUserId",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Clinic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "PatientRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Clinic",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_ApplicationUserId",
                table: "PatientRecords",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_Users_ApplicationUserId",
                table: "PatientRecords",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
