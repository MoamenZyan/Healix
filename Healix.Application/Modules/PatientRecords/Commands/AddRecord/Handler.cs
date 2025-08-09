using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddRecordHandler : IRequestHandler<AddRecordCommand, PatientRecordDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Service _s3Service;

    public AddRecordHandler(IApplicationDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<PatientRecordDto> Handle(
        AddRecordCommand request,
        CancellationToken cancellationToken
    )
    {
        var record = new PatientRecord()
        {
            Id = Guid.NewGuid(),
            Notes = request.Notes,
            DoctorName = request.DoctorName,
            Date = request.Date,
            Speciality = request.Speciality,
            ClinicName = request.ClinicName,
            MedicalHistoryType = request.MedicalHistoryType,
            IsFirstTime = request.IsFirstTime,
            MedicalDiagnoses = request.MedicalDiagnoses,
            TestName = request.TestName,
            ScanType = request.ScanType,
            ScanName = request.ScanName,
            FacilityName = request.FacilityName,
            ScannedPart = request.ScannedPart,
            ProcedureName = request.ProcedureName,
            LogType = request.LogType,
            DiseaseName = request.DiseaseName,
            SupervisedBy = request.SupervisedBy,
            RiskLevel = request.RiskLevel,
            LastTimeDiagnosed = request.LastTimeDiagnosed,
            FamilyInfectionSpreadLevel = request.FamilyInfectionSpreadLevel,
            Allergen = request.Allergen,
            AllergyStatus = request.AllergyStatus,
            ReactionSeverity = request.ReactionSeverity,
            PatientId = request.PatientId,
        };

        await _context.PatientRecords.AddAsync(record);

        if (request.Medicines != null)
        {
            var medicines = new List<Medicine>();
            foreach (var medicine in request.Medicines)
            {
                medicines.Add(
                    new Medicine()
                    {
                        MedicineName = medicine.MedicineName,
                        EndDate = medicine.EndDate,
                        Id = Guid.NewGuid(),
                        StartDate = medicine.StartDate,
                        Frequency = medicine.Frequency,
                        PatientRecordId = record.Id,
                    }
                );
            }

            await _context.Medicines.AddRangeAsync(medicines);
        }

        if (request.Files != null)
        {
            var files = new List<UploadedFile>();
            foreach (var file in request.Files)
            {
                var url = await _s3Service.UploadFile(file);
                files.Add(new UploadedFile() { PatientRecordId = record.Id, FileUrl = url });
            }

            await _context.UploadedFiles.AddRangeAsync(files);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return request.MedicalHistoryType switch
        {
            MedicalHistoryType.MedicalVisit => record.ToMedicalVisitDto(),
            MedicalHistoryType.LapTests => record.ToLabTestDto(),
            MedicalHistoryType.XRays => record.ToXrayDto(),
            MedicalHistoryType.Surgery => record.ToSurgeryDto(),
            MedicalHistoryType.Logs => record.ToLogsDto(),
            MedicalHistoryType.Chronic => record.ToChronicDiseaseDto(),
            MedicalHistoryType.Hereditary => record.ToHereditaryDiseaseDto(),
            MedicalHistoryType.Allergy => record.ToAllergyDto(),
            _ => throw new BadRequestException("Medical History Type Not Supported."),
        };
    }
}
