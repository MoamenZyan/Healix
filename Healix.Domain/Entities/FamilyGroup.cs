namespace Healix.Domain.Entities
{
    public class FamilyGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Guid FamilySummaryId { get; set; }
        public virtual FamilySummary FamilySummary { get; set; } = null!;

        public virtual List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();

        public FamilyGroupMinimalDto ToMinimalDto()
        {
            return new FamilyGroupMinimalDto()
            {
                Id = this.Id,
                Name = this.Name,
                Code = this.Code,
            };
        }

        public FamilyGroupDto ToDto()
        {
            return new FamilyGroupDto()
            {
                Id = this.Id,
                Name = this.Name,
                Code = this.Code,
                Members = this.Members.Select(x => x.ToLoginUserDto()).ToList(),
            };
        }
    }

    public class FamilyGroupMinimalDto : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class FamilyGroupDto : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual List<LoginUserDto> Members { get; set; } = null!;
    }
}
