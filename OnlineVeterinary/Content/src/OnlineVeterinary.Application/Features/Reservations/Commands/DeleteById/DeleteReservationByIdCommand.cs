using System;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.DeleteById
{
    public record DeleteReservationByIdCommand(Guid Id, string userId, string Role) : IRequest<ErrorOr<string>>;
   
}
