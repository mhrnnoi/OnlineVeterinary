using System;
using ErrorOr;
using MediatR;

using OnlineVeterinary.Application.Features.Common;

namespace OnlineVeterinary.Application.Features.Reservations.Commands.Add
{
    public record AddReservationCommand(Guid PetId, Guid DoctorId, string CareGiverId) : IRequest<ErrorOr<ReservationDTO>>;

}
