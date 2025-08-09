using Healix.Domain.Entities;
using MediatR;

public class AddClinicHandler : IRequestHandler<AddClinicCommand, ClinicDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Service _s3Service;

    public AddClinicHandler(IApplicationDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<ClinicDto> Handle(
        AddClinicCommand request,
        CancellationToken cancellationToken
    )
    {
        var clinic = new Clinic()
        {
            Id = Guid.NewGuid(),
            Hotline = request.HotLine,
            City = request.City,
            Location = request.Location,
            Name = request.Name,
            Fees = request.Fees,
            DoctorId = (Guid)request.CurrentUserId!,
        };

        if (request.Photo != null)
        {
            var url = await _s3Service.UploadFile(request.Photo);

            clinic.PhotoUrl = url;
        }

        await _context.Clinics.AddAsync(clinic, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return clinic.ToDto();
    }
}
