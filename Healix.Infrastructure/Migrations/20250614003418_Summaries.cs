using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Summaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PatientSummaryId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FamilySummaryId",
                table: "FamilyGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FamilySummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilySummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilySummaries_FamilyGroups_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "FamilyGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientSummaries_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilySummaries_FamilyId",
                table: "FamilySummaries",
                column: "FamilyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientSummaries_PatientId",
                table: "PatientSummaries",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilySummaries");

            migrationBuilder.DropTable(
                name: "PatientSummaries");

            migrationBuilder.DropColumn(
                name: "PatientSummaryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FamilySummaryId",
                table: "FamilyGroups");
        }
    }
}
