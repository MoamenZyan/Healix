using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetClinicHandler : IRequestHandler<GetClinicQuery, List<ClinicDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClinicHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClinicDto>> Handle(
        GetClinicQuery request,
        CancellationToken cancellationToken
    )
    {
        List<ClinicDto>? clinics;
        if (request.ClinicId != null)
        {
            clinics = await _context
                .Clinics.Where(x => x.Id == request.ClinicId)
                .Select(x => x.ToDto())
                .ToListAsync(cancellationToken);
        }
        else
        {
            clinics = await _context
                .Clinics.Where(x => x.DoctorId == x.DoctorId)
                .Select(x => x.ToDto())
                .ToListAsync(cancellationToken);
        }

        return clinics;
    }
}
