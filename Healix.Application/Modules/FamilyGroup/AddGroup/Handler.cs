using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

public class AddFamilyGroupHandler : IRequestHandler<AddFamilyGroupCommand, FamilyGroupMinimalDto>
{
    private readonly IApplicationDbContext _context;

    public AddFamilyGroupHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FamilyGroupMinimalDto> Handle(
        AddFamilyGroupCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Id == request.CurrentUserId,
            cancellationToken
        );

        if (user == null)
            throw new NotFoundException("user not found!");

        var familyGroup = new FamilyGroup()
        {
            Id = Guid.NewGuid(),
            Code = OtpSecret.GetRandomOTP(),
            Name = request.Name,
        };

        user.FamilyGroupId = familyGroup.Id;

        await _context.FamilyGroups.AddAsync(familyGroup, cancellationToken);
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return familyGroup.ToMinimalDto();
    }
}
