using Healix.Domain.Entities;
using MediatR;

public class AddUserToFamilyGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
    public Guid? CurrentUserId { get; set; }
}
