using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class EditDoctorScheduleHandler
    : IRequestHandler<EditDoctorScheduleCommand, DoctorScheduleDto>
{
    private readonly IApplicationDbContext _context;

    public EditDoctorScheduleHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorScheduleDto> Handle(
        EditDoctorScheduleCommand request,
        CancellationToken cancellationToken
    )
    {
        var schedule = await _context.DoctorSchedules.FirstOrDefaultAsync(
            x => x.Id == request.ScheduleId,
            cancellationToken
        );

        if (schedule == null)
            throw new NotFoundException("schedule not found to edit!");

        schedule.SatFrom = request.SatFrom;
        schedule.SatTo = request.SatTo;

        schedule.SunFrom = request.SunFrom;
        schedule.SunTo = request.SunTo;

        schedule.MonFrom = request.MonFrom;
        schedule.MonTo = request.MonTo;

        schedule.TueFrom = request.TueFrom;
        schedule.TueTo = request.TueTo;

        schedule.WedFrom = request.WedFrom;
        schedule.WedTo = request.WedTo;

        schedule.ThuFrom = request.ThuFrom;
        schedule.ThuTo = request.ThuTo;

        schedule.FriFrom = request.FriFrom;
        schedule.FriTo = request.FriTo;

        _context.DoctorSchedules.Update(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return schedule.ToDto();
    }
}
