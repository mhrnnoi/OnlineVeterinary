using FluentValidation;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Add
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
        }
    }
}