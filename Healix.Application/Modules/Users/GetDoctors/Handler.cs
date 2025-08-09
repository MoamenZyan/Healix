using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetDoctorHandler : IRequestHandler<GetDoctorQuery, List<DoctorUserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorUserDto>> Handle(
        GetDoctorQuery request,
        CancellationToken cancellationToken
    )
    {
        List<ApplicationUser> doctors = new List<ApplicationUser>();

        var query = _context
            .Users.Include(x => x.Clinic)
            .Include(x => x.DoctorCertificates)
            .Include(x => x.DoctorAppointments);

        if (request.DoctorId != null)
        {
            doctors = await query
                .Where(x => x.Id == request.DoctorId)
                .ToListAsync(cancellationToken);
        }
        else
        {
            if (request.DoctorSpeciality != null)
            {
                doctors = await query
                    .Where(x =>
                        x.UserType == Healix.Domain.Enums.UserType.Doctor
                        && x.DoctorSpeciality == request.DoctorSpeciality
                    )
                    .ToListAsync(cancellationToken);
            }
            else
            {
                doctors = await query
                    .Where(x => x.UserType == Healix.Domain.Enums.UserType.Doctor)
                    .ToListAsync(cancellationToken);
            }
        }

        return doctors.Select(x => x.ToDoctorDto()).ToList();
    }
}
