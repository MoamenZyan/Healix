using Healix.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ChatBot> ChatBots { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatMessageFile> ChatMessageFiles { get; set; }
    public DbSet<PatientRecord> PatientRecords { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<UploadedFile> UploadedFiles { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<UserChronicDisease> ChronicDiseases { get; set; }
    public DbSet<FamilyGroup> FamilyGroups { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<DoctorCertificate> DoctorCertificates { get; set; }
    public DbSet<FamilySummary> FamilySummaries { get; set; }
    public DbSet<PatientSummary> PatientSummaries { get; set; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
