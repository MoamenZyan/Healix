using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healix.Infrastructure.Configurations.EntitiesConfigurations
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.From).IsRequired();

            builder.Property(x => x.To).IsRequired();

            builder.Property(x => x.Day).IsRequired();

            builder
                .Property(x => x.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), x!)
                );

            // Relations
            builder
                .HasOne(x => x.Patient)
                .WithMany(x => x.PatientAppointments)
                .HasForeignKey(x => x.PatientId)
                .IsRequired();

            builder
                .HasOne(x => x.Doctor)
                .WithMany(x => x.DoctorAppointments)
                .HasForeignKey(x => x.DoctorId)
                .IsRequired();
        }
    }
}
