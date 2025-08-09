using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class EditClinicHandler : IRequestHandler<EditClinicCommand, ClinicDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Service _s3Service;

    public EditClinicHandler(IApplicationDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<ClinicDto> Handle(
        EditClinicCommand request,
        CancellationToken cancellationToken
    )
    {
        var clinic = await _context.Clinics.FirstOrDefaultAsync(x => x.Id == request.ClinicId);
        if (clinic == null)
            throw new NotFoundException("Clinic not found to edit.");

        if (request.Photo != null)
        {
            var url = await _s3Service.UploadFile(request.Photo);
            clinic.PhotoUrl = url;
        }

        if (request.City != null)
            clinic.City = request.City;

        if (request.Hotline != null)
            clinic.Hotline = request.Hotline;

        if (request.Name != null)
            clinic.Name = request.Name;

        if (request.Location != null)
            clinic.Location = request.Location;

        _context.Clinics.Update(clinic);

        await _context.SaveChangesAsync(cancellationToken);

        return clinic.ToDto();
    }
}
