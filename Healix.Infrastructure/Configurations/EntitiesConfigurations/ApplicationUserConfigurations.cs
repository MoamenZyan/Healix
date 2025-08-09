using Healix.Domain.Entities;
using Healix.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healix.Infrastructure.Configurations.EntitiesConfigurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.PracticeLicenseUrl).IsRequired(false);

            builder.Property(x => x.YearsOfExperience).IsRequired(false);

            builder.Property(x => x.ProfessionalTitle).IsRequired(false);

            builder
                .Property(x => x.DoctorSpeciality)
                .HasConversion(
                    x => x.ToString(),
                    x => (DoctorSpeciality)Enum.Parse(typeof(DoctorSpeciality), x)
                );

            builder
                .Property(x => x.UserType)
                .HasConversion(x => x.ToString(), x => (UserType)Enum.Parse(typeof(UserType), x));

            builder.Property(x => x.Fname).IsRequired();

            builder.Property(x => x.Lname);

            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.PhoneNumber).IsRequired();

            builder.Property(x => x.Address).IsRequired();

            builder.Property(x => x.PhotoUrl).IsRequired(false);

            builder.Property(x => x.Height).IsRequired();

            builder.Property(x => x.Weight).IsRequired();

            builder.Property(x => x.DateOfBirth).IsRequired();

            builder.Property(x => x.NationalId).IsRequired();

            builder.Property(x => x.FamilyGroupId).IsRequired(false);

            builder
                .Property(x => x.BloodType)
                .HasConversion(x => x.ToString(), x => (BloodType)Enum.Parse(typeof(BloodType), x));

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
                .HasOne(x => x.FamilyGroup)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.FamilyGroupId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
