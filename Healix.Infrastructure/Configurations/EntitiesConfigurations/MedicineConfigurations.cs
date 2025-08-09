using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicineConfigurations : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("Medicines");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.MedicineName).IsRequired();

        builder.Property(x => x.Frequency).IsRequired(false);

        builder.Property(x => x.EndDate).IsRequired(false);

        builder.Property(x => x.StartDate).IsRequired(false);

        // Relations
        builder
            .HasOne(x => x.PatientRecord)
            .WithMany(x => x.Medicines)
            .HasForeignKey(x => x.PatientRecordId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
