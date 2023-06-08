using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<Visitor> Visitors { get; set; } = new List<Visitor>();
        public List<DateTime> ReservedTimes { get; set; } = new List<DateTime>();
        public double SuccesfulVisits { get; set; } = 0;
        public double Likes { get; set; } = 0;
        public double Dislikes { get; set; } = 0;
        public string Comments { get; set; }
        public PetEnum Specific { get; set; }
        public bool IsAvailable { get; set; }
        public LocationEnum Location { get; set; }
        public string Bio { get; set; }

    }
}