using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientSummaryConfigurations : IEntityTypeConfiguration<PatientSummary>
{
    public void Configure(EntityTypeBuilder<PatientSummary> builder)
    {
        builder.ToTable("PatientSummaries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Summary).IsRequired();

        // Relations
        builder
            .HasOne(x => x.Patient)
            .WithOne(x => x.PatientSummary)
            .HasForeignKey<PatientSummary>(x => x.PatientId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
