using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Commands.Add
{
    public record AddReservationCommand(Guid PetId) : IRequest<ReservationDTO>;

}
