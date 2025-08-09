using MediatR;

public class GetClinicQuery : IRequest<List<ClinicDto>>
{
    public Guid? ClinicId { get; set; }
    public Guid? DoctorId { get; set; }
}
