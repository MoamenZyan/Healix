using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetRecordsHandler : IRequestHandler<GetRecordsQuery, List<PatientRecordWrapper>>
{
    private readonly IApplicationDbContext _context;

    public GetRecordsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<List<PatientRecordWrapper>> IRequestHandler<
        GetRecordsQuery,
        List<PatientRecordWrapper>
    >.Handle(GetRecordsQuery request, CancellationToken cancellationToken)
    {
        List<PatientRecordWrapper>? records = null;
        if (request.RecordId == null)
        {
            var rawRecords = await _context
                .PatientRecords.Where(x =>
                    x.MedicalHistoryType == request.RecordType && x.PatientId == request.PatientId
                )
                .ToListAsync();

            records = rawRecords
                .Select(x => new PatientRecordWrapper
                {
                    RecordType = request.RecordType,
                    RecordDto = request.RecordType switch
                    {
                        MedicalHistoryType.MedicalVisit => x.ToMedicalVisitDto(),
                        MedicalHistoryType.LapTests => x.ToLabTestDto(),
                        MedicalHistoryType.XRays => x.ToXrayDto(),
                        MedicalHistoryType.Surgery => x.ToSurgeryDto(),
                        MedicalHistoryType.Logs => x.ToLogsDto(),
                        MedicalHistoryType.Chronic => x.ToChronicDiseaseDto(),
                        MedicalHistoryType.Hereditary => x.ToHereditaryDiseaseDto(),
                        MedicalHistoryType.Allergy => x.ToAllergyDto(),
                        _ => throw new BadRequestException("Medical History Type Not Supported."),
                    },
                })
                .ToList();
        }
        else
        {
            var rawRecords = await _context
                .PatientRecords.Where(x =>
                    x.MedicalHistoryType == request.RecordType
                    && x.Id == request.RecordId
                    && x.PatientId == request.PatientId
                )
                .ToListAsync();

            records = rawRecords
                .Select(x => new PatientRecordWrapper
                {
                    RecordType = request.RecordType,
                    RecordDto = request.RecordType switch
                    {
                        MedicalHistoryType.MedicalVisit => x.ToMedicalVisitDto(),
                        MedicalHistoryType.LapTests => x.ToLabTestDto(),
                        MedicalHistoryType.XRays => x.ToXrayDto(),
                        MedicalHistoryType.Surgery => x.ToSurgeryDto(),
                        MedicalHistoryType.Logs => x.ToLogsDto(),
                        MedicalHistoryType.Chronic => x.ToChronicDiseaseDto(),
                        MedicalHistoryType.Hereditary => x.ToHereditaryDiseaseDto(),
                        MedicalHistoryType.Allergy => x.ToAllergyDto(),
                        _ => throw new BadRequestException("Medical History Type Not Supported."),
                    },
                })
                .ToList();
        }

        return records;
    }
}
