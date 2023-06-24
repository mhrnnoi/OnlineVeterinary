using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(x=> x.NewEmail).NotEmpty().EmailAddress().WithMessage("plz enter valid email");

        }
    }
}