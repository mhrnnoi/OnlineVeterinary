using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x=> x.NewPassword).NotEmpty();
            RuleFor(x=> x.NewPassword).MinimumLength(8);
            RuleFor(x => x.NewPassword).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")
            .WithMessage(@"password must have Minimum eight characters, at least one upper case English letter,
                           one lower case English letter, one number and one special character");
        }
    }
}