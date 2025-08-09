using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorScheduleConfigurations : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.ToTable("DoctorSchedules");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.SatFrom).IsRequired(false);
        builder.Property(x => x.SatTo).IsRequired(false);

        builder.Property(x => x.SunFrom).IsRequired(false);
        builder.Property(x => x.SunTo).IsRequired(false);

        builder.Property(x => x.MonFrom).IsRequired(false);
        builder.Property(x => x.MonTo).IsRequired(false);

        builder.Property(x => x.TueFrom).IsRequired(false);
        builder.Property(x => x.TueTo).IsRequired(false);

        builder.Property(x => x.WedFrom).IsRequired(false);
        builder.Property(x => x.WedTo).IsRequired(false);

        builder.Property(x => x.ThuFrom).IsRequired(false);
        builder.Property(x => x.ThuTo).IsRequired(false);

        builder.Property(x => x.FriFrom).IsRequired(false);
        builder.Property(x => x.FriTo).IsRequired(false);

        // Relations
        builder
            .HasOne(x => x.Doctor)
            .WithOne(x => x.DoctorSchedule)
            .HasForeignKey<DoctorSchedule>(x => x.DoctorId)
            .IsRequired(false);
    }
}
