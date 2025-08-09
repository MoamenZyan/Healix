using Healix.Domain.Entities;

public class Clinic : BaseEntity
{
    public Location Location { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;
    public string Hotline { get; set; } = null!;
    public string City { get; set; } = null!;
    public double Fees { get; set; }

    public Guid DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; } = null!;

    public ClinicDto ToDto()
    {
        var dto = new ClinicDto()
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            Location = this.Location,
            Name = this.Name,
            City = this.City,
            Fees = this.Fees,
            Hotline = this.Hotline,
            PhotoUrl = this.PhotoUrl,
        };

        return dto;
    }
}

public class ClinicDto : BaseEntity
{
    public Location Location { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Hotline { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;
    public double Fees { get; set; }
}
