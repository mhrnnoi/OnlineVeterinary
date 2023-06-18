using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Domain.ReservedTime.Entities
{
    public class ReservedTime
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PetId { get; set; }
        public Guid CareGiverId { get; set; }
        public DateTime TimeOfReservation { get; set; }
        public string DrLastName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string CareGiverLastName { get; set; } = string.Empty;
    }
}