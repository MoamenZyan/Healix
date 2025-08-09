using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorCertificateConfigurations : IEntityTypeConfiguration<DoctorCertificate>
{
    public void Configure(EntityTypeBuilder<DoctorCertificate> builder)
    {
        builder.ToTable("DoctorCertificates");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.CertificateUrl).IsRequired();

        // Relations
        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.DoctorCertificates)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}
