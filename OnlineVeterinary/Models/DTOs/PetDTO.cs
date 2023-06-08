using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs
{
    public class PetDTO
    {
        public string Username { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PetEnum PetType { get; set; }
        public string Sickness { get; set; }

    }
}