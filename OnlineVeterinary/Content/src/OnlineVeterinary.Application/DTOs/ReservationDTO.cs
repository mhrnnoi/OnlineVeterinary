using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Application.DTOs
{
    public class ReservationDTO
    {
        public DateOnly DateOfReservation { get; set; }
        public TimeOnly TimeOfReservation { get; set; }
        public string DrLastName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string CareGiverLastName { get; set; } = string.Empty;
    }
}