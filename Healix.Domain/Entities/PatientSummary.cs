using Healix.Domain.Entities;

public class PatientSummary : BaseEntity
{
    public string Summary { get; set; } = null!;
    public Guid PatientId { get; set; }
    public virtual ApplicationUser Patient { get; set; } = null!;

    public PatientSummaryDto ToDto()
    {
        return new PatientSummaryDto()
        {
            Summary = this.Summary,
            Id = this.Id,
            IsDeleted = this.IsDeleted,
            CreatedAt = this.CreatedAt,
            PatientId = this.PatientId,
        };
    }
}

public class PatientSummaryDto : BaseEntity
{
    public string Summary { get; set; } = null!;
    public Guid PatientId { get; set; }
}
