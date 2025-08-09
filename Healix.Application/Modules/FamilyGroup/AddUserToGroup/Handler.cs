using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddUserToFamilyGroupHandler : IRequestHandler<AddUserToFamilyGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public AddUserToFamilyGroupHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        AddUserToFamilyGroupCommand request,
        CancellationToken cancellationToken
    )
    {
        var familyGroup = await _context.FamilyGroups.FirstOrDefaultAsync(
            x => x.Id == request.GroupId,
            cancellationToken
        );

        if (familyGroup == null)
            throw new NotFoundException("family not found to add user");

        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Id == request.CurrentUserId,
            cancellationToken
        );

        if (user == null)
            throw new NotFoundException("user not found");

        user.FamilyGroupId = familyGroup.Id;
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
