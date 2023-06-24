using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById
{
    public record DeleteReservationByIdCommand(Guid Id, string userId) : IRequest<ErrorOr<string>>;
   
}
