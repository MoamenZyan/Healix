using FluentValidation;

namespace Healix.Application.Modules;

public class LoginUserCommandValidator : AbstractValidator<UserSignupCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("email required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("password required");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("confirm passsword required");
    }
}
