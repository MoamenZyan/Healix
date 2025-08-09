using MediatR;
using Microsoft.AspNetCore.Http;

public class EditClinicCommand : IRequest<ClinicDto>
{
    public Guid ClinicId { get; set; }
    public Location? Location { get; set; } = null!;
    public string? Hotline { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public IFormFile? Photo { get; set; } = null!;
}
