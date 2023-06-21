using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace OnlineVeterinary.Application.CareGivers.Commands.Add
{
    public class AddCareGiverCommandValidator : AbstractValidator<AddCareGiverCommand>
    {
        public AddCareGiverCommandValidator()
        {
            // RuleFor(x=> x.Email).NotEmpty().WithMessage("error email is empty").WithName("empty email");;
            // RuleFor(x=> x.Email).EmailAddress().WithMessage("invalid email").WithName("invalid email");

            // RuleFor(y => y.FirstName).NotEmpty()
            
        }
    }
}