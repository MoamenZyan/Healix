using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SetAppointmentStatusHandler
    : IRequestHandler<SetAppointmentStatusCommand, AppointmentMinimalDto>
{
    private readonly IApplicationDbContext _context;

    public SetAppointmentStatusHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentMinimalDto> Handle(
        SetAppointmentStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var appointment = await _context.Appointments.FirstOrDefaultAsync(
            x => x.Id == request.AppointmentId,
            cancellationToken
        );

        if (appointment == null)
            throw new NotFoundException("appointment not found!");

        appointment.Status = request.Status;

        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        return appointment.ToMinimalDto();
    }
}
