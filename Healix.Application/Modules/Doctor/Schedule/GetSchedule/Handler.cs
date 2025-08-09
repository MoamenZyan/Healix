using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetDoctorScheduleHandler : IRequestHandler<GetDoctorScheduleQuery, DoctorScheduleDto?>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorScheduleHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorScheduleDto?> Handle(
        GetDoctorScheduleQuery request,
        CancellationToken cancellationToken
    )
    {
        DoctorSchedule? schedule;
        if (request.DoctorId != null)
        {
            schedule = await _context.DoctorSchedules.FirstOrDefaultAsync(
                x => x.DoctorId == request.DoctorId,
                cancellationToken
            );

            return schedule?.ToDto();
        }
        else
        {
            schedule = await _context.DoctorSchedules.FirstOrDefaultAsync(
                x => x.DoctorId == request.CurrentUserId,
                cancellationToken
            );

            return schedule?.ToDto();
        }
    }
}
