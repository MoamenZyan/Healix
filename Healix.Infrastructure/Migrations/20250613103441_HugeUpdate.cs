using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HugeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessionalTitle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fees",
                table: "Clinic",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoctorCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificateUrl = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorCertificates_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCertificates_DoctorId",
                table: "DoctorCertificates",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorCertificates");

            migrationBuilder.DropColumn(
                name: "ProfessionalTitle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Fees",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");
        }
    }
}
