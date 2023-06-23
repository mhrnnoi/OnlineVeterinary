using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.Pet.Enums;

namespace OnlineVeterinary.Domain.Pet.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string CareGiverId { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PetType { get; set; } = string.Empty;
        public string CareGiverName { get; set; } = string.Empty;

    }
}