using MediatR;

public class PatientSummaryCommand : IRequest<string>
{
    public Guid? CurrentUserId { get; set; }
    public Guid? PatientId { get; set; }
}
