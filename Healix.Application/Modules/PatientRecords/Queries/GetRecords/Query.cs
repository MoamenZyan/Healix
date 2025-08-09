using Healix.Domain.Entities;
using MediatR;

public class GetRecordsQuery : IRequest<List<PatientRecordWrapper>>
{
    public Guid? PatientId { get; set; }
    public Guid? RecordId { get; set; }
    public MedicalHistoryType RecordType { get; set; }
}
