using Healix.Domain.Entities;

public class FamilySummary : BaseEntity
{
    public string Summary { get; set; } = null!;
    public Guid FamilyId { get; set; }
    public virtual FamilyGroup Family { get; set; } = null!;

    public FamilySummaryDto ToDto()
    {
        return new FamilySummaryDto()
        {
            Summary = this.Summary,
            Id = this.Id,
            IsDeleted = this.IsDeleted,
            CreatedAt = this.CreatedAt,
            FamilyId = this.FamilyId,
        };
    }
}

public class FamilySummaryDto : BaseEntity
{
    public string Summary { get; set; } = null!;
    public Guid FamilyId { get; set; }
}
