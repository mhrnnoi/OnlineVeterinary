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
        public Guid CareGiverId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public PetType PetType { get; set; }

    }
}