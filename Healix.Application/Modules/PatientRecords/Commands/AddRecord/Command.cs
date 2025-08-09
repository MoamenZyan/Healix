using System.Text.Json.Serialization;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

public class AddRecordCommand : IRequest<PatientRecordDto>
{
    public Guid PatientId { get; set; }
    public string Notes { get; set; } = null!;
    public string? DoctorName { get; set; }
    public DateOnly Date { get; set; }
    public string? Speciality { get; set; }
    public string? ClinicName { get; set; } = null!;
    public MedicalHistoryType? MedicalHistoryType { get; set; }
    public bool? IsFirstTime { get; set; } = true;
    public string? MedicalDiagnoses { get; set; } = null!;
    public string? ScanType { get; set; }
    public string? ScanName { get; set; }
    public string? FacilityName { get; set; }
    public string? ScannedPart { get; set; }
    public string? ProcedureName { get; set; }
    public string? TestName { get; set; } = null!;
    public string? LogType { get; set; }
    public string? DiseaseName { get; set; } = null!;
    public string? SupervisedBy { get; set; } = null!;
    public RiskLevel? RiskLevel { get; set; } = null!;
    public string? LastTimeDiagnosed { get; set; }
    public string? FamilyInfectionSpreadLevel { get; set; }
    public string? Allergen { get; set; } = null!;
    public AllergyStatus? AllergyStatus { get; set; }
    public ReactionSeverity? ReactionSeverity { get; set; }
    public List<MedicineDto>? Medicines { get; set; } = null!;
    public List<IFormFile>? Files { get; set; } = null!;
}
