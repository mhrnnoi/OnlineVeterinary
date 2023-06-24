using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs.Incoming
{
    public class DoctorCompleteProfileRegister
    {
        public string FullName { get; set; }
        public string Bio { get; set; }
        public PetEnum Specific { get; set; }
        public LocationEnum Location { get; set; }



    }
}