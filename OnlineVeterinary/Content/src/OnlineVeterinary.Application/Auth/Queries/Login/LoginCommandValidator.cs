using FluentValidation;

namespace OnlineVeterinary.Application.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
        }
    }
}