using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models
{
    public class Pet
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sickness { get; set; }
        public PetEnum PetType { get; set; }
        public int TimesOfCured { get; set; } = 0;
        // public List<Doctor> DoctorId; this is many to many
        // public List<DateTime> ReservedTimes = new List<DateTime>(); many to many
        // public List<CareGiver> CareGivers = new List<CareGiver>(); many to many
        public string MyProperty { get; set; }

        public Doctor FavoriteDoctorId { get; set; }


    }
}