using MediatR;

public class AddDoctorScheduleCommand : IRequest<DoctorScheduleDto>
{
    public TimeOnly SatFrom { get; set; }
    public TimeOnly SatTo { get; set; }

    public TimeOnly SunFrom { get; set; }
    public TimeOnly SunTo { get; set; }

    public TimeOnly MonFrom { get; set; }
    public TimeOnly MonTo { get; set; }

    public TimeOnly TueFrom { get; set; }
    public TimeOnly TueTo { get; set; }

    public TimeOnly WedFrom { get; set; }
    public TimeOnly WedTo { get; set; }

    public TimeOnly ThuFrom { get; set; }
    public TimeOnly ThuTo { get; set; }

    public TimeOnly FriFrom { get; set; }
    public TimeOnly FriTo { get; set; }

    public Guid? CurrentUserId { get; set; }
}
