using Healix.Domain.Entities;

public class DoctorSchedule : BaseEntity
{
    public Guid DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; } = null!;

    public TimeOnly? SatFrom { get; set; }
    public TimeOnly? SatTo { get; set; }

    public TimeOnly? SunFrom { get; set; }
    public TimeOnly? SunTo { get; set; }

    public TimeOnly? MonFrom { get; set; }
    public TimeOnly? MonTo { get; set; }

    public TimeOnly? TueFrom { get; set; }
    public TimeOnly? TueTo { get; set; }

    public TimeOnly? WedFrom { get; set; }
    public TimeOnly? WedTo { get; set; }

    public TimeOnly? ThuFrom { get; set; }
    public TimeOnly? ThuTo { get; set; }

    public TimeOnly? FriFrom { get; set; }
    public TimeOnly? FriTo { get; set; }

    public DoctorScheduleDto ToDto()
    {
        return new DoctorScheduleDto()
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            IsDeleted = this.IsDeleted,
            SatFrom = this.SatFrom,
            SatTo = this.SatTo,
            SunFrom = this.SunFrom,
            SunTo = this.SunTo,
            MonFrom = this.MonFrom,
            MonTo = this.MonTo,
            TueFrom = this.TueFrom,
            TueTo = this.TueTo,
            WedFrom = this.WedFrom,
            WedTo = this.WedTo,
            ThuFrom = this.ThuFrom,
            ThuTo = this.ThuTo,
            FriFrom = this.FriFrom,
            FriTo = this.FriTo,
            DoctorId = this.DoctorId,
        };
    }
}

public class DoctorScheduleDto : BaseEntity
{
    public Guid DoctorId { get; set; }

    public TimeOnly? SatFrom { get; set; }
    public TimeOnly? SatTo { get; set; }

    public TimeOnly? SunFrom { get; set; }
    public TimeOnly? SunTo { get; set; }

    public TimeOnly? MonFrom { get; set; }
    public TimeOnly? MonTo { get; set; }

    public TimeOnly? TueFrom { get; set; }
    public TimeOnly? TueTo { get; set; }

    public TimeOnly? WedFrom { get; set; }
    public TimeOnly? WedTo { get; set; }

    public TimeOnly? ThuFrom { get; set; }
    public TimeOnly? ThuTo { get; set; }

    public TimeOnly? FriFrom { get; set; }
    public TimeOnly? FriTo { get; set; }
}
