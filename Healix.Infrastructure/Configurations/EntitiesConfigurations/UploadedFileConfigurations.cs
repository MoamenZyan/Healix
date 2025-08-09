using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UploadedFileConfigurations : IEntityTypeConfiguration<UploadedFile>
{
    public void Configure(EntityTypeBuilder<UploadedFile> builder)
    {
        builder.ToTable("UploadedFiles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.FileUrl).IsRequired();

        // Relations
        builder
            .HasOne(x => x.PatientRecord)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.PatientRecordId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
