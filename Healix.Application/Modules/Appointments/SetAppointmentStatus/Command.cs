using Healix.Domain.Entities;
using MediatR;

public class SetAppointmentStatusCommand : IRequest<AppointmentMinimalDto>
{
    public AppointmentStatus Status { get; set; }
    public Guid AppointmentId { get; set; }
}
