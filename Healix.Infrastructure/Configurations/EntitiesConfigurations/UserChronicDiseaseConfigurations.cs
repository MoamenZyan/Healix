using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserChronicDiseaseConfigurations : IEntityTypeConfiguration<UserChronicDisease>
{
    public void Configure(EntityTypeBuilder<UserChronicDisease> builder)
    {
        builder.ToTable("UserChronicDiseases");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();

        // Relations
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.ChronicDiseases)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
