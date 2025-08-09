using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_Users_DoctorId",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "PatientRecords");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "PatientRecords",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientRecords_DoctorId",
                table: "PatientRecords",
                newName: "IX_PatientRecords_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "Allergen",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllergyStatus",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PatientRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DiseaseName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyInfectionSpreadLevel",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstTime",
                table: "PatientRecords",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastTimeDiagnosed",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalDiagnoses",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistoryType",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcedureName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReactionSeverity",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskLevel",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScanName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScanType",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScannedPart",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisedBy",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "PatientRecords",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<string>(type: "text", nullable: false),
                    Fees = table.Column<string>(type: "text", nullable: false),
                    Experience = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinic_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineName = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PatientRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_PatientRecords_PatientRecordId",
                        column: x => x.PatientRecordId,
                        principalTable: "PatientRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    PatientRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_PatientRecords_PatientRecordId",
                        column: x => x.PatientRecordId,
                        principalTable: "PatientRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_DoctorId",
                table: "Clinic",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PatientRecordId",
                table: "Medicines",
                column: "PatientRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_PatientRecordId",
                table: "UploadedFiles",
                column: "PatientRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_Users_ApplicationUserId",
                table: "PatientRecords",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_Users_ApplicationUserId",
                table: "PatientRecords");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "Allergen",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "AllergyStatus",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "DiseaseName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "FamilyInfectionSpreadLevel",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "IsFirstTime",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "LastTimeDiagnosed",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "LogType",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "MedicalDiagnoses",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryType",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ProcedureName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ReactionSeverity",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ScanName",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ScanType",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "ScannedPart",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "SupervisedBy",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "TestName",
                table: "PatientRecords");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "PatientRecords",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientRecords_ApplicationUserId",
                table: "PatientRecords",
                newName: "IX_PatientRecords_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "PatientRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_Users_DoctorId",
                table: "PatientRecords",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
