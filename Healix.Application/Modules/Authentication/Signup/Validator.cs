using FluentValidation;

namespace Healix.Application.Modules;

public class SignupUserCommandValidator : AbstractValidator<UserSignupCommand>
{
    public SignupUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("email required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("password required");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("confirm passsword required");
    }
}
