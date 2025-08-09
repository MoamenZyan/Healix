using MediatR;

public class DeleteClinicCommand : IRequest
{
    public Guid ClinicId { get; set; }
}
