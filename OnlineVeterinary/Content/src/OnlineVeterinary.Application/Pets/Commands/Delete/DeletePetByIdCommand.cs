using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Pets.Commands.Delete
{
    public record DeletePetByIdCommand(Guid Id, string CareGiverId)  : IRequest<ErrorOr<string>>;
   
}
