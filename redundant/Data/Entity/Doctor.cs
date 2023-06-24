using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVeterinary.Data.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public double SuccesfulVisits { get; set; }
        public double Likes { get; set; }
        public double Dislikes { get; set; }
        public PetEnum Specific { get; set; }
        public bool IsAvailable { get; set; }
        public LocationEnum Location { get; set; }
        public string Bio { get; set; }

    }
}