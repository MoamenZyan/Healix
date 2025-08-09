using Healix.Domain.Entities;
using MediatR;

public class GetFamilyGroupQuery : IRequest<FamilyGroupDto>
{
    public string? Code { get; set; }
    public Guid? CurrentUserId { get; set; }
}
