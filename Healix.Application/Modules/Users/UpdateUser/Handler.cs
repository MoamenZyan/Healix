using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ApplicationUserWrapper>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Service _s3Service;

    public UpdateUserHandler(IApplicationDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<ApplicationUserWrapper> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        string? photoUrl = null;
        if (request.User.Photo != null)
        {
            var url = await _s3Service.UploadFile(request.User.Photo);
            photoUrl = url;
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id);
        if (user == null)
            throw new NotFoundException("user not found to update.");

        user = request.User.ToEntity(user, photoUrl);

        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return new ApplicationUserWrapper(user.UserType, user);
    }
}
