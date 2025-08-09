using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddAppointmentHandler : IRequestHandler<AddAppointmentCommand, AppointmentMinimalDto>
{
    private readonly IApplicationDbContext _context;

    public AddAppointmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentMinimalDto> Handle(
        AddAppointmentCommand request,
        CancellationToken cancellationToken
    )
    {
        var doctorSchedule = await _context.DoctorSchedules.FirstOrDefaultAsync(
            x => x.DoctorId == request.DoctorId,
            cancellationToken
        );

        if (doctorSchedule == null)
            throw new NotFoundException("doctor schedule not found to book appointment!");

        var isValid = false;

        switch (request.Day.DayOfWeek)
        {
            case DayOfWeek.Saturday:
            {
                if (
                    doctorSchedule.SatFrom != null
                    && doctorSchedule.SatTo != null
                    && doctorSchedule.SatFrom <= request.From
                    && doctorSchedule.SatTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Sunday:
            {
                if (
                    doctorSchedule.SunFrom != null
                    && doctorSchedule.SunTo != null
                    && doctorSchedule.SunFrom <= request.From
                    && doctorSchedule.SunTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Monday:
            {
                if (
                    doctorSchedule.MonFrom != null
                    && doctorSchedule.MonTo != null
                    && doctorSchedule.MonFrom <= request.From
                    && doctorSchedule.MonTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Tuesday:
            {
                if (
                    doctorSchedule.TueFrom != null
                    && doctorSchedule.TueTo != null
                    && doctorSchedule.TueFrom <= request.From
                    && doctorSchedule.TueTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Wednesday:
            {
                if (
                    doctorSchedule.WedFrom != null
                    && doctorSchedule.WedTo != null
                    && doctorSchedule.WedFrom <= request.From
                    && doctorSchedule.WedTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Thursday:
            {
                if (
                    doctorSchedule.ThuFrom != null
                    && doctorSchedule.ThuTo != null
                    && doctorSchedule.ThuFrom <= request.From
                    && doctorSchedule.ThuTo >= request.To
                )
                    isValid = true;

                break;
            }
            case DayOfWeek.Friday:
            {
                if (
                    doctorSchedule.FriFrom != null
                    && doctorSchedule.FriTo != null
                    && doctorSchedule.FriFrom <= request.From
                    && doctorSchedule.FriTo >= request.To
                )
                    isValid = true;

                break;
            }
        }

        if (isValid)
        {
            var appointment = new Appointment()
            {
                From = request.From,
                To = request.To,
                Day = request.Day,
                Id = Guid.NewGuid(),
                DoctorId = request.DoctorId,
                Status = AppointmentStatus.Pending,
                PatientId = (Guid)request.CurrentUserId!,
            };

            await _context.Appointments.AddAsync(appointment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return appointment.ToMinimalDto();
        }

        throw new BadRequestException("doctor doesn't available at this time!");
    }
}
