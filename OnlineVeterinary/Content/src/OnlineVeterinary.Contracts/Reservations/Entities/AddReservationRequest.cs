using System;

namespace OnlineVeterinary.Contracts.Reservations.Request
{
    public record AddReservationRequest(Guid PetId, Guid CareGiverId);
    
}
