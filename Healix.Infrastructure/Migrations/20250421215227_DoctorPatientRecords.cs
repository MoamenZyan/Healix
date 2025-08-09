using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorPatientRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_DoctorId",
                table: "PatientRecords",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_Users_DoctorId",
                table: "PatientRecords",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_Users_DoctorId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecords_DoctorId",
                table: "PatientRecords");
        }
    }
}
