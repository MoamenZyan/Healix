using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FamilyGroupConfigurations : IEntityTypeConfiguration<FamilyGroup>
{
    public void Configure(EntityTypeBuilder<FamilyGroup> builder)
    {
        builder.ToTable("FamilyGroups");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Code).IsRequired();
    }
}
