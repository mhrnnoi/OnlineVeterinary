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
        public string Email { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<DateTime> ReservedTimes = new List<DateTime>();




    }
}