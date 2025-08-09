using Healix.Domain.Entities;
using MediatR;

public class UpdateUserCommand : IRequest<ApplicationUserWrapper>
{
    public UpdateUserDto User { get; set; } = null!;
}
