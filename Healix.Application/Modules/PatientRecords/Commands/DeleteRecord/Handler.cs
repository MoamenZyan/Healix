using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteRecordHandler : IRequestHandler<DeleteRecordCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecordHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
    {
        await _context.PatientRecords.Where(x => x.Id == request.RecordId).ExecuteDeleteAsync();
    }
}
