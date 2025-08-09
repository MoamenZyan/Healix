using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddDoctorScheduleHandler : IRequestHandler<AddDoctorScheduleCommand, DoctorScheduleDto>
{
    private readonly IApplicationDbContext _context;

    public AddDoctorScheduleHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorScheduleDto> Handle(
        AddDoctorScheduleCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _context
            .Users.Include(x => x.DoctorSchedule)
            .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

        if (user == null)
            throw new NotFoundException("user not found!");

        if (user.DoctorSchedule != null)
            throw new ConflictException("doctor already have schedule");

        if (user.UserType == Healix.Domain.Enums.UserType.Doctor)
        {
            var doctorSchedule = new DoctorSchedule()
            {
                DoctorId = user.Id,
                SatFrom = request.SatFrom,
                SatTo = request.SatTo,
                SunFrom = request.SunFrom,
                SunTo = request.SunTo,
                MonFrom = request.MonFrom,
                MonTo = request.MonTo,
                TueFrom = request.TueFrom,
                TueTo = request.TueTo,
                WedFrom = request.WedFrom,
                WedTo = request.WedTo,
                ThuFrom = request.ThuFrom,
                ThuTo = request.ThuTo,
                FriFrom = request.FriFrom,
                FriTo = request.FriTo,
            };

            await _context.DoctorSchedules.AddAsync(doctorSchedule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return doctorSchedule.ToDto();
        }

        throw new BadRequestException("error when creating doctor schedule!");
    }
}
