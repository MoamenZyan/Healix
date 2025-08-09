using MediatR;

public class FamilySummaryCommand : IRequest<string>
{
    public Guid? CurrentUserId { get; set; }
    public Guid? FamilyId { get; set; }
}
