using Healix.Domain.Entities;
using Healix.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Healix.Domain.Entities
{
    public class PatientRecord : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string Notes { get; set; } = null!;
        public string? DoctorName { get; set; }
        public DateOnly Date { get; set; }
        public string? Speciality { get; set; }
        public string? ClinicName { get; set; } = null!;
        public MedicalHistoryType? MedicalHistoryType { get; set; }
        public bool AiProcessed { get; set; } = false;

        // Medical Visit
        public bool? IsFirstTime { get; set; } = true;
        public string? MedicalDiagnoses { get; set; } = null!;

        // Lap Test
        public string? TestName { get; set; } = null!;

        // X-Rays
        public string? ScanType { get; set; }
        public string? ScanName { get; set; }
        public string? FacilityName { get; set; }
        public string? ScannedPart { get; set; }

        // Surgery
        public string? ProcedureName { get; set; }

        // Logs
        public string? LogType { get; set; }

        // Chronic Disease
        public string? DiseaseName { get; set; } = null!;
        public string? SupervisedBy { get; set; } = null!;
        public RiskLevel? RiskLevel { get; set; } = null!;

        // Hereditary Disease
        public string? LastTimeDiagnosed { get; set; }
        public string? FamilyInfectionSpreadLevel { get; set; }

        // Allergy
        public string? Allergen { get; set; } = null!;
        public AllergyStatus? AllergyStatus { get; set; }
        public ReactionSeverity? ReactionSeverity { get; set; }

        public virtual ApplicationUser Patient { get; set; } = null!;
        public virtual List<Medicine> Medicines { get; set; } = null!;
        public virtual List<UploadedFile> Files { get; set; } = null!;

        public PatientRecordMedicalVisitDto ToMedicalVisitDto()
        {
            var record = new PatientRecordMedicalVisitDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                IsFirstTime = this.IsFirstTime,
                DoctorName = this.DoctorName,
                Speciality = this.Speciality,
                ClinicName = this.ClinicName,
                MedicalDiagnoses = this.MedicalDiagnoses,
                Notes = this.Notes,
                Prescription = this.Medicines?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.MedicalVisit,
            };

            return record;
        }

        public PatientRecordLabTestDto ToLabTestDto()
        {
            var dto = new PatientRecordLabTestDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                TestName = this.TestName,
                Speciality = this.Speciality,
                ClinicName = this.ClinicName,
                Notes = this.Notes,
                Reports = this.Files?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.LapTests,
            };

            return dto;
        }

        public PatientRecordXrayDto ToXrayDto()
        {
            var dto = new PatientRecordXrayDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                ScanType = this.ScanType,
                ScanName = this.ScanName,
                FacilityName = this.FacilityName,
                ScannedPart = this.ScannedPart,
                Notes = this.Notes,
                Reports = this.Files?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.XRays,
            };

            return dto;
        }

        public PatientRecordSurgeryDto ToSurgeryDto()
        {
            var dto = new PatientRecordSurgeryDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                Speciality = this.Speciality,
                ProcedureName = this.ProcedureName,
                DoctorName = this.DoctorName,
                ClinicName = this.ClinicName,
                Notes = this.Notes,
                Reports = this.Files?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.Surgery,
            };

            return dto;
        }

        public PatientRecordLogsDto ToLogsDto()
        {
            var dto = new PatientRecordLogsDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                Speciality = this.Speciality,
                Notes = this.Notes,
                LogType = this.LogType,
                Reports = this.Files?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.Logs,
            };

            return dto;
        }

        public PatientRecordChronicDiseaseDto ToChronicDiseaseDto()
        {
            var dto = new PatientRecordChronicDiseaseDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                Speciality = this.Speciality,
                DiseaseName = this.DiseaseName,
                RiskLevel = this.RiskLevel,
                SupervisedBy = this.SupervisedBy,
                Medicines = this.Medicines?.Select(x => x.ToDto()).ToList(),
                Notes = this.Notes,
                Reports = this.Files?.Select(x => x.ToDto()).ToList(),
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.Chronic,
            };

            return dto;
        }

        public PatientRecordHereditaryDiseaseDto ToHereditaryDiseaseDto()
        {
            var dto = new PatientRecordHereditaryDiseaseDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                Speciality = this.Speciality,
                DiseaseName = this.DiseaseName,
                LastTimeDiagnosed = this.LastTimeDiagnosed,
                FamilyInfectionSpreadLevel = this.FamilyInfectionSpreadLevel,
                Medicines = this.Medicines?.Select(x => x.ToDto()).ToList(),
                Notes = this.Notes,
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.Hereditary,
            };

            return dto;
        }

        public PatientRecordAllergyDto ToAllergyDto()
        {
            var dto = new PatientRecordAllergyDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Date = this.Date,
                Allergen = this.Allergen,
                ReactionSeverity = this.ReactionSeverity,
                AllergyStatus = this.AllergyStatus,
                Notes = this.Notes,
                PatientId = this.PatientId,
                MedicalHistoryType = global::MedicalHistoryType.Allergy,
            };

            return dto;
        }
    }

    public class PatientRecordMedicalVisitDto : PatientRecordDto
    {
        public bool? IsFirstTime { get; set; } = true;
        public string? DoctorName { get; set; }
        public string? Speciality { get; set; }
        public string? ClinicName { get; set; } = null!;
        public string? MedicalDiagnoses { get; set; } = null!;
        public List<MedicineDto>? Prescription { get; set; } = null!;
    }

    public class PatientRecordLabTestDto : PatientRecordDto
    {
        public string? TestName { get; set; }
        public string? Speciality { get; set; }
        public string? ClinicName { get; set; } = null!;
        public List<UploadedFileDto>? Reports { get; set; } = null!;
    }

    public class PatientRecordXrayDto : PatientRecordDto
    {
        public string? ScanType { get; set; }
        public string? ScanName { get; set; }
        public string? FacilityName { get; set; }
        public string? ScannedPart { get; set; }
        public List<UploadedFileDto>? Reports { get; set; } = null!;
    }

    public class PatientRecordSurgeryDto : PatientRecordDto
    {
        public string? Speciality { get; set; }
        public string? ProcedureName { get; set; }
        public string? DoctorName { get; set; }
        public string? ClinicName { get; set; }
        public List<UploadedFileDto>? Reports { get; set; } = null!;
    }

    public class PatientRecordLogsDto : PatientRecordDto
    {
        public string? LogType { get; set; }
        public string? Speciality { get; set; }
        public List<UploadedFileDto>? Reports { get; set; } = null!;
    }

    public class PatientRecordChronicDiseaseDto : PatientRecordDto
    {
        public string? Speciality { get; set; }
        public string? DiseaseName { get; set; } = null!;
        public RiskLevel? RiskLevel { get; set; } = null!;
        public string? SupervisedBy { get; set; } = null!;
        public List<MedicineDto>? Medicines { get; set; } = null!;
        public List<UploadedFileDto>? Reports { get; set; } = null!;
    }

    public class PatientRecordHereditaryDiseaseDto : PatientRecordDto
    {
        public string? Speciality { get; set; }
        public string? DiseaseName { get; set; } = null!;
        public string? LastTimeDiagnosed { get; set; }
        public string? FamilyInfectionSpreadLevel { get; set; }
        public List<MedicineDto>? Medicines { get; set; } = null!;
    }

    public class PatientRecordAllergyDto : PatientRecordDto
    {
        public string? Allergen { get; set; } = null!;
        public ReactionSeverity? ReactionSeverity { get; set; }
        public AllergyStatus? AllergyStatus { get; set; }
    }

    public abstract class PatientRecordDto : BaseEntity
    {
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
        public Guid PatientId { get; set; }
        public MedicalHistoryType MedicalHistoryType { get; set; }
    }

    public class PatientRecordWrapper
    {
        public MedicalHistoryType RecordType { get; set; }
        public object RecordDto { get; set; } = null!;
    }
}
