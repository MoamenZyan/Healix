using Healix.Domain.Entities;
using MediatR;

public class GetDoctorAppointmentsQuery : IRequest<List<AppointmentsByDayDto>>
{
    public Guid? CurrentUserId { get; set; }
}
