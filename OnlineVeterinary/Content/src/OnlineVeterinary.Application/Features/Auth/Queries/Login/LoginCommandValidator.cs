using FluentValidation;

namespace OnlineVeterinary.Application.Features.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
        }
    }
}