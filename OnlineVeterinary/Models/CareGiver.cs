using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models
{
    
    public class CareGiver
    {
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public List<DateTime> ReservedTimes = new List<DateTime>();




    }
}