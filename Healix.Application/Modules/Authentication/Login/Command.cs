using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;

namespace Healix.Application.Modules;

public class UserLoginCommand : IRequest<(string, ApplicationUserWrapper)>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
