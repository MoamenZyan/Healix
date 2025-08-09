using Healix.Domain.Entities;

public class UserChronicDisease : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    public UserChronicDiseaseDto ToDto()
    {
        var dto = new UserChronicDiseaseDto() { Name = this.Name, Description = this.Description };

        return dto;
    }
}

public class UserChronicDiseaseDto : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
