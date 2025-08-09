using Healix.Domain.Entities;
using MediatR;

public class AddFamilyGroupCommand : IRequest<FamilyGroupMinimalDto>
{
    public string Name { get; set; }
    public Guid? CurrentUserId { get; set; }
}
