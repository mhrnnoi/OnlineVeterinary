using System;
using MediatR;

namespace OnlineVeterinary.Application.Pets.Commands.Delete
{
    public record DeletePetByIdCommand(Guid Id)  : IRequest<string>;
   
}
