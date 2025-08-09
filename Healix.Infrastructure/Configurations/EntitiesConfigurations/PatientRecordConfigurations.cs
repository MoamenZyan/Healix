using Healix.Domain.Entities;
using Healix.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healix.Infrastructure.Configurations.EntitiesConfigurations
{
    public class PatientRecordConfigurations : IEntityTypeConfiguration<PatientRecord>
    {
        public void Configure(EntityTypeBuilder<PatientRecord> builder)
        {
            builder.ToTable("PatientRecords");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Notes).IsRequired();

            builder.Property(x => x.DoctorName).IsRequired(false);
            builder.Property(x => x.TestName).IsRequired(false);

            builder.Property(x => x.Date).IsRequired();

            builder.Property(x => x.Speciality).IsRequired(false);

            builder.Property(x => x.ClinicName).IsRequired(false);

            builder
                .Property(x => x.MedicalHistoryType)
                .HasConversion(
                    x => x.ToString(),
                    x => (MedicalHistoryType)Enum.Parse(typeof(MedicalHistoryType), x!)
                )
                .IsRequired(false);

            builder.Property(x => x.IsFirstTime).IsRequired(false);

            builder.Property(x => x.MedicalDiagnoses).IsRequired(false);

            builder.Property(x => x.ScanType).IsRequired(false);

            builder.Property(x => x.ScanName).IsRequired(false);

            builder.Property(x => x.FacilityName).IsRequired(false);

            builder.Property(x => x.ScannedPart).IsRequired(false);

            builder.Property(x => x.ProcedureName).IsRequired(false);

            builder.Property(x => x.LogType).IsRequired(false);

            builder.Property(x => x.DiseaseName).IsRequired(false);

            builder.Property(x => x.SupervisedBy).IsRequired(false);

            builder.Property(x => x.AiProcessed).IsRequired();

            builder
                .Property(x => x.RiskLevel)
                .HasConversion(x => x.ToString(), x => (RiskLevel)Enum.Parse(typeof(RiskLevel), x!))
                .IsRequired(false);

            builder.Property(x => x.LastTimeDiagnosed).IsRequired(false);

            builder.Property(x => x.Allergen).IsRequired(false);
            builder.Property(x => x.FamilyInfectionSpreadLevel).IsRequired(false);

            builder
                .Property(x => x.AllergyStatus)
                .HasConversion(
                    x => x.ToString(),
                    x => (AllergyStatus)Enum.Parse(typeof(AllergyStatus), x!)
                )
                .IsRequired(false);

            builder
                .Property(x => x.ReactionSeverity)
                .HasConversion(
                    x => x.ToString(),
                    x => (ReactionSeverity)Enum.Parse(typeof(ReactionSeverity), x!)
                )
                .IsRequired(false);

            // Relations
            builder
                .HasOne(x => x.Patient)
                .WithMany(x => x.PatientRecords)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
