using System;
using FluentValidation;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Delete
{
    public class DeletePetByIdCommandValidator : AbstractValidator<DeletePetByIdCommand>
    {
        public DeletePetByIdCommandValidator()
        {
        }
    }
}
