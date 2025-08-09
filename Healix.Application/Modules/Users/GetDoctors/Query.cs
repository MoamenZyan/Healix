using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;

public class GetDoctorQuery : IRequest<List<DoctorUserDto>>
{
    public DoctorSpeciality? DoctorSpeciality { get; set; }
    public Guid? DoctorId { get; set; }
}
