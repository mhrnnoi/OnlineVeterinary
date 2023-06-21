using System;
using MediatR;

namespace OnlineVeterinary.Application.Reservations.Commands.DeleteById
{
    public record DeleteReservationByIdCommand(Guid Id) : IRequest<string>;
   
}
