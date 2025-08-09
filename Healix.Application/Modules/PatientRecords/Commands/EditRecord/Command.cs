using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

public class EditRecordCommand : IRequest<PatientRecordDto>
{
    public Guid RecordId { get; set; }
    public string? Notes { get; set; }
    public IFormFile File { get; set; } = null!;
}
