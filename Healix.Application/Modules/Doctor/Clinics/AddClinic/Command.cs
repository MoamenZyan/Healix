using MediatR;
using Microsoft.AspNetCore.Http;

public class AddClinicCommand : IRequest<ClinicDto>
{
    public Location Location { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string HotLine { get; set; } = null!;
    public string City { get; set; } = null!;
    public double Fees { get; set; }
    public IFormFile? Photo { get; set; } = null!;
    public Guid? CurrentUserId { get; set; }
}
