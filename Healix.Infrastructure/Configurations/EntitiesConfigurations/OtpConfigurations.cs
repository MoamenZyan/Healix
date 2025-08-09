using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OtpConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.ToTable("Otps");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion(x => x.ToString(), x => (OtpStatus)Enum.Parse(typeof(OtpStatus), x));

        builder.Property(x => x.Secret).IsRequired();
    }
}
