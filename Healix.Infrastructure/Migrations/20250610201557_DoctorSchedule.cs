using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorScheduleId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SatFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    SatTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    SunFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    SunTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    MonFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    MonTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    TueFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    TueTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    WedFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    WedTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ThuFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ThuTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    FriFrom = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    FriTo = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "Users");
        }
    }
}
