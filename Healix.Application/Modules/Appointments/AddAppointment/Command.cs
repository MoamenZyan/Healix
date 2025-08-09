using Healix.Domain.Entities;
using MediatR;

public class AddAppointmentCommand : IRequest<AppointmentMinimalDto>
{
    public DateOnly Day { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public Guid DoctorId { get; set; }
    public Guid? CurrentUserId { get; set; }
}
