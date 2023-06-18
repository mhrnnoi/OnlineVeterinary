using System;
using MediatR;

namespace OnlineVeterinary.Application.CareGivers.Commands.DeleteById
{
    public record DeleteCareGiverByIdCommand(Guid Id) : IRequest<string>;
   
}
