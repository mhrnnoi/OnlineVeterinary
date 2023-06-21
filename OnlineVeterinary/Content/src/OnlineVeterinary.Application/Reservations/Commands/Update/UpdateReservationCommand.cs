using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Reservations.Commands.Update
{
    public record UpdateReservationCommand(Guid Id, Guid DoctorId, Guid PetId, Guid CareGiverId, DateOnly DateOfReservation, TimeOnly TimeOfReservation) : IRequest<ReservationDTO>;
    
}
