using MediatR;

public class GetDoctorScheduleQuery : IRequest<DoctorScheduleDto>
{
    public Guid? DoctorId { get; set; }
    public Guid? CurrentUserId { get; set; }
}
