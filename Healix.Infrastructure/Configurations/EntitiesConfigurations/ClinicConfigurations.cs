using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClinicConfigurations : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.ToTable("Clinic");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Hotline).IsRequired();

        builder.Property(x => x.City).IsRequired();

        builder.Property(x => x.PhotoUrl).IsRequired(false);

        builder.Property(x => x.Fees).IsRequired();

        builder.OwnsOne(
            au => au.Location,
            location =>
            {
                location.Property(l => l.Lat).HasColumnName("Latitude").IsRequired();

                location.Property(l => l.Long).HasColumnName("Longitude").IsRequired();
            }
        );

        // Relations
        builder
            .HasOne(x => x.Doctor)
            .WithOne(x => x.Clinic)
            .HasForeignKey<Clinic>(x => x.DoctorId)
            .IsRequired(false);
    }
}
