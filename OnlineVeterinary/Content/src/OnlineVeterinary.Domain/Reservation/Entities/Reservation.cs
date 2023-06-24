using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Domain.Reservation.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PetId { get; set; } 
        public string CareGiverId { get; set; } = string.Empty;
        public DateTime DateOfReservation { get; set; }
        public string DrLastName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string CareGiverLastName { get; set; } = string.Empty;
    }
}