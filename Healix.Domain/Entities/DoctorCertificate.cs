using Healix.Domain.Entities;

public class DoctorCertificate : BaseEntity
{
    public string CertificateUrl { get; set; } = null!;
    public Guid DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; } = null!;

    public DoctorCertificateDto ToDto()
    {
        return new DoctorCertificateDto()
        {
            Id = this.Id,
            IsDeleted = this.IsDeleted,
            CreatedAt = this.CreatedAt,
            CertificateUrl = this.CertificateUrl,
            DoctorId = this.DoctorId,
        };
    }
}

public class DoctorCertificateDto : BaseEntity
{
    public string CertificateUrl { get; set; } = null!;
    public Guid DoctorId { get; set; }
}
