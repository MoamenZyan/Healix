using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FamilySummaryConfigurations : IEntityTypeConfiguration<FamilySummary>
{
    public void Configure(EntityTypeBuilder<FamilySummary> builder)
    {
        builder.ToTable("FamilySummaries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Summary).IsRequired();

        // Relations
        builder
            .HasOne(x => x.Family)
            .WithOne(x => x.FamilySummary)
            .HasForeignKey<FamilySummary>(x => x.FamilyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
