using MediatR;

public class DeleteRecordCommand : IRequest
{
    public Guid? RecordId { get; set; }
    public Guid? CurrentUserId { get; set; }
}
