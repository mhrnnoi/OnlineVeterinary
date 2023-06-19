using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Pets.Commands.Update
{
    public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
    {
        public UpdatePetCommandValidator()
        {
        }
    }
}
