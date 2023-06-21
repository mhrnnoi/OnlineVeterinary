using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Commands.AddCustom
{
    public record AddReservationCustomCommand(Guid DoctorId, Guid PetId, DateOnly DateOfReservation, TimeOnly TimeOfReservation) : IRequest<ReservationDTO>;

}
