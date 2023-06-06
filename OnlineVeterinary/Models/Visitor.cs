using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string NameOfPet { get; set; }
        public Pet Pet { get; set; }
        public CareGiver CareGiver { get; set; }
        public string Sickness { get; set; }
        public PetEnum PetType { get; set; }
        public DateTime ReservedTime { get; set; }
        public Doctor Doctor { get; set; }

    }
}