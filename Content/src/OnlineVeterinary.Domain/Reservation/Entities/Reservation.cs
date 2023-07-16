

using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVeterinary.Domain.Reservation.Entities
{
    [Table("Reservations")]

    public class Reservation
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("DoctorId")]
        public Guid DoctorId { get; set; }
        [Column("PetId")]
        public Guid PetId { get; set; } 
        [Column("CareGiverId")]
        public Guid CareGiverId { get; set;}
        [Column("DateOfReservation")]
        public DateTime DateOfReservation { get; set; }
        [Column("DrLastName")]
        public string DrLastName { get; set; } = string.Empty;
        [Column("PetName")]
        public string PetName { get; set; } = string.Empty;
        [Column("CareGiverLastName")]
        public string CareGiverLastName { get; set; } = string.Empty;
    }
}