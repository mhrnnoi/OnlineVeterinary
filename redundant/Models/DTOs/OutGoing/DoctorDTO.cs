using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs
{
    public class DoctorDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public double SuccesfulVisits { get; set; }
        public double Likes { get; set; }
        public double Dislikes { get; set; }
        public string Comments { get; set; }
        public PetEnum Specific { get; set; }
        public bool IsAvailable { get; set; }
        public LocationEnum Location { get; set; }
        public string Bio { get; set; }



    }
}