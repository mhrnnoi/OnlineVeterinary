using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Pets.Commands.Delete
{
    public class DeletePetByIdCommandValidator : AbstractValidator<DeletePetByIdCommand>
    {
        public DeletePetByIdCommandValidator()
        {
        }
    }
}
