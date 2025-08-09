using MediatR;
using Microsoft.AspNetCore.Http;

public class AddDoctorCertificateCommand : IRequest
{
    public Guid? CurrentUserId { get; set; }
    public IFormFile Certificate { get; set; } = null!;
}
