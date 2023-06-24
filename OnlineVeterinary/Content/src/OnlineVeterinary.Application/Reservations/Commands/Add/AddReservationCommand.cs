using System;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Commands.Add
{
    public record AddReservationCommand(Guid PetId, Guid DoctorId, string CareGiverId) : IRequest<ErrorOr<ReservationDTO>>;

}
