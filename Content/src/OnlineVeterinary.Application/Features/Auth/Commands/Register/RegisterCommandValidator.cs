using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x=> x.Role).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
            RuleFor(x=> x.Email).NotEmpty().EmailAddress().WithMessage("plz enter valid email");
            RuleFor(x=> x.Password).NotEmpty();
            RuleFor(x=> x.Password).MinimumLength(8);
            RuleFor(x => x.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")
            .WithMessage(@"password must have Minimum eight characters, at least one upper case English letter,
                           one lower case English letter, one number and one special character");
            
        }
    }
}