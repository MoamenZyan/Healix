using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

public class GetFamilyGroupHandler : IRequestHandler<GetFamilyGroupQuery, FamilyGroupDto>
{
    private readonly IApplicationDbContext _context;

    public GetFamilyGroupHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FamilyGroupDto> Handle(
        GetFamilyGroupQuery request,
        CancellationToken cancellationToken
    )
    {
        FamilyGroupDto? familyGroup = null;

        if (request.Code != null)
        {
            var family = await _context
                .FamilyGroups.Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

            familyGroup = family?.ToDto();
        }
        else
        {
            var user = await _context
                .Users.Include(x => x.FamilyGroup)
                .ThenInclude(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (user == null)
                throw new NotFoundException("user not found!");

            familyGroup = user.FamilyGroup.ToDto();
        }

        return familyGroup;
    }
}
