using Healix.Application.Exceptions;
using MediatR;

public class AddDoctorCertificateHandler : IRequestHandler<AddDoctorCertificateCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Service _s3Service;

    public AddDoctorCertificateHandler(IApplicationDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task Handle(
        AddDoctorCertificateCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.Certificate != null)
        {
            var url = await _s3Service.UploadFile(request.Certificate);
            var certificate = new DoctorCertificate()
            {
                CertificateUrl = url,
                DoctorId = (Guid)request.CurrentUserId!,
            };

            await _context.DoctorCertificates.AddAsync(certificate, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        throw new BadRequestException("certificate not found!");
    }
}
