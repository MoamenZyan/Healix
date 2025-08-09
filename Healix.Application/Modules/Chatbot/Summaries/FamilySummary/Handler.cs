using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class FamilySummaryHandler : IRequestHandler<FamilySummaryCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IGeminiService _geminiService;

    public FamilySummaryHandler(IApplicationDbContext context, IGeminiService geminiService)
    {
        _context = context;
        _geminiService = geminiService;
    }

    public async Task<string> Handle(
        FamilySummaryCommand request,
        CancellationToken cancellationToken
    )
    {
        var familyId = Guid.Empty;
        var family = new FamilyGroup();

        if (request.FamilyId != null)
        {
            familyId = (Guid)request.FamilyId;
            family = await _context
                .FamilyGroups.Include(x => x.Members)
                .Include(x => x.FamilySummary)
                .FirstOrDefaultAsync(x => x.Id == familyId, cancellationToken);
        }
        else
        {
            var user = await _context
                .Users.Include(x => x.FamilyGroup)
                .ThenInclude(x => x.Members)
                .Include(x => x.FamilyGroup)
                .ThenInclude(x => x.FamilySummary)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (user == null)
                throw new NotFoundException("user not found");

            family = user.FamilyGroup;
            familyId = family.Id;
        }

        if (family == null)
            throw new NotFoundException("family group not found");

        var contents = new List<Content>();

        foreach (var patient in family.Members)
        {
            var summary = await _context.PatientSummaries.FirstOrDefaultAsync(
                x => x.PatientId == patient.Id,
                cancellationToken
            );

            if (summary != null)
                contents.Add(
                    await ConvertMessageToContent.ConvertMessage(
                        new ChatMessage() { Content = summary.Summary, IsUser = true }
                    )
                );
        }

        if (family.FamilySummary == null)
        {
            var result = await _geminiService.GetFamilySummary(contents);

            var familySummary = new FamilySummary()
            {
                Id = Guid.NewGuid(),
                FamilyId = (Guid)familyId!,
                Summary = result.Parts.First().Text,
            };

            await _context.FamilySummaries.AddAsync(familySummary);

            await _context.SaveChangesAsync(cancellationToken);
            return result.Parts.First().Text;
        }
        else
            return family.FamilySummary.Summary;
    }
}
