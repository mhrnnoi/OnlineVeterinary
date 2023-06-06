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
        public DateTime DateOfBirth { get; set; }
        public bool IsSick { get; set; }
        public string Sickness { get; set; }
        public CareGiver CareGiver { get; set; }
        public PetEnum PetType { get; set; }
        public int TimesOfCured { get; set; } = 0;
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<DateTime> ReservedTimes = new List<DateTime>();

        public Doctor FavoriteDoctor { get; set; }


    }
}