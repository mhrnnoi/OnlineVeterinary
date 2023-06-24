using System.Text.RegularExpressions;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Add
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        
        public AddPetCommandValidator()
        {
           RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now)
           .WithMessage("your pet date of birth is invalid");
           RuleFor(x => x.PetType).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1)
           .WithMessage("you can select just 1 or 0");
        }
    }
}