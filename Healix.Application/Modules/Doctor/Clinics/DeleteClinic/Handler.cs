using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteClinicHandler : IRequestHandler<DeleteClinicCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClinicHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClinicCommand request, CancellationToken cancellationToken)
    {
        await _context
            .Clinics.Where(x => x.Id == request.ClinicId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
