using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Data.Entity;

namespace OnlineVeterinary.Models
{
    public class Pet
    {
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sickness { get; set; }
        public PetEnum PetType { get; set; }
        public int TimesOfCured { get; set; }
        public string FavoriteDoctorUsername { get; set; }


    }
}