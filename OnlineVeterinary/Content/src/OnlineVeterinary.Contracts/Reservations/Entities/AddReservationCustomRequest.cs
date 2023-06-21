using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Contracts.Reservations.Entities
{
    public record AddReservationCustomRequest(Guid DoctorId,
Guid PetId,
Guid CareGiverId,
DateOnly DateOfReservation,
 TimeOnly TimeOfReservation);
    
}