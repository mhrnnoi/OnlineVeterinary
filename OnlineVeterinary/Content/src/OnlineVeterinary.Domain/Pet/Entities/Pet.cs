using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Domain.Pet.Entities
{
    [Table("Pets")]
    public class Pet
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("CareGiverId")]
        public Guid CareGiverId { get; set; }
        [Column("Name")]
        public string Name { get; set; } = string.Empty;
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Column("PetType")]
        public string PetType { get; set; } = string.Empty;
        [Column("CareGiverLastName")]
        public string CareGiverLastName { get; set; } = string.Empty;

    }
}