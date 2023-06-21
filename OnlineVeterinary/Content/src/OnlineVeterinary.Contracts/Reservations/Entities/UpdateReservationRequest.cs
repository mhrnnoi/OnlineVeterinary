using System;

namespace OnlineVeterinary.Contracts.Reservations.Request
{
    public record UpdateReservationRequest(Guid Id, Guid DoctorId, Guid PetId, Guid CareGiverId, DateOnly DateOfReservation, TimeOnly TimeOfReservation);

}
