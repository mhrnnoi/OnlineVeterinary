using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs.Incoming
{
    public class PetRegisterDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sickness { get; set; }
        public PetEnum PetType { get; set; }
    }
}