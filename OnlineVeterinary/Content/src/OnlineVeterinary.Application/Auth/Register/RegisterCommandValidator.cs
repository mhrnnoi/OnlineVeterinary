using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x=> x.RoleType).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
            RuleFor(x=> x.Email).EmailAddress().WithMessage("plz enter valid email");
            RuleFor(x=> x.Password).NotEmpty();
            RuleFor(x=> x.Password).MinimumLength(8);
            RuleFor(x => x.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")
            .WithMessage("password must have Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character");
            
        }
    }
}