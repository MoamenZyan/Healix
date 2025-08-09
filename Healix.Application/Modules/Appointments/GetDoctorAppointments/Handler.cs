using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class GetDoctorAppointmentsHandler
    : IRequestHandler<GetDoctorAppointmentsQuery, List<AppointmentsByDayDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorAppointmentsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentsByDayDto>> Handle(
        GetDoctorAppointmentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _context
            .Users.Include(u => u.DoctorAppointments)
            .ThenInclude(a => a.Patient)
            .FirstOrDefaultAsync(u => u.Id == request.CurrentUserId, cancellationToken);

        if (user == null)
            throw new NotFoundException("user not found");

        var appointmentsByDay = user
            .DoctorAppointments?.Select(a => a.ToDto())
            .GroupBy(a => a.Day)
            .Select(x =>
            {
                return new AppointmentsByDayDto() { Day = x.Key, Appointments = x.ToList() };
            })
            .ToList();

        return appointmentsByDay;
    }
}
