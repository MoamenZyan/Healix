using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class PatientSummaryHandler : IRequestHandler<PatientSummaryCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IGeminiService _geminiService;

    public PatientSummaryHandler(IApplicationDbContext context, IGeminiService geminiService)
    {
        _context = context;
        _geminiService = geminiService;
    }

    public async Task<string> Handle(
        PatientSummaryCommand request,
        CancellationToken cancellationToken
    )
    {
        Guid patientId = Guid.Empty;
        if (request.PatientId != null)
            patientId = (Guid)request.PatientId;
        else
            patientId = (Guid)request.CurrentUserId!;

        var records = await _context
            .PatientRecords.Where(x => x.PatientId == patientId && x.AiProcessed == false)
            .ToListAsync(cancellationToken);

        var summary = await _context.PatientSummaries.FirstOrDefaultAsync(
            x => x.PatientId == patientId,
            cancellationToken
        );

        if (records == null || records.Count() == 0)
        {
            if (summary == null)
                throw new NotFoundException("summary not found!");

            return summary.Summary!;
        }

        var messages = new List<ChatMessage>();
        var contents = new List<Content>();

        foreach (var record in records)
        {
            record.AiProcessed = true;

            dynamic dto = null!;

            switch (record.MedicalHistoryType)
            {
                case MedicalHistoryType.MedicalVisit:
                {
                    dto = record.ToMedicalVisitDto();
                    break;
                }
                case MedicalHistoryType.LapTests:
                {
                    dto = record.ToLabTestDto();
                    break;
                }
                case MedicalHistoryType.Allergy:
                {
                    dto = record.ToAllergyDto();
                    break;
                }
                case MedicalHistoryType.Chronic:
                {
                    dto = record.ToChronicDiseaseDto();
                    break;
                }
                case MedicalHistoryType.Hereditary:
                {
                    dto = record.ToHereditaryDiseaseDto();
                    break;
                }
                case MedicalHistoryType.XRays:
                {
                    dto = record.ToXrayDto();
                    break;
                }
                case MedicalHistoryType.Surgery:
                {
                    dto = record.ToSurgeryDto();
                    break;
                }
                case MedicalHistoryType.Logs:
                {
                    dto = record.ToLogsDto();
                    break;
                }
            }

            var message = new ChatMessage()
            {
                Content = JsonConvert.SerializeObject(dto),
                IsUser = true,
            };

            var content = await ConvertMessageToContent.ConvertMessage(message);

            contents.Add(content);
        }

        if (summary != null)
        {
            var summaryMessage = new ChatMessage() { Content = summary.Summary, IsUser = true };

            contents.Add(await ConvertMessageToContent.ConvertMessage(summaryMessage));
        }

        var result = await _geminiService.GetPatientSummary(contents);

        var patientSummary = new PatientSummary()
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            Summary = result.Parts.First().Text,
        };

        await _context.PatientSummaries.AddAsync(patientSummary, cancellationToken);

        _context.PatientRecords.UpdateRange(records);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Parts.First().Text;
    }
}
