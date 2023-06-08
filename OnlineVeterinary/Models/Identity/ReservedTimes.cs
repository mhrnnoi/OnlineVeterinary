using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.Identity
{
    public class ReservedTimes
    {
        
        public int Id { get; set; }
        public int DrId { get; set; }
        public int PetId { get; set; }
        public int CgId { get; set; }
        public int ReservedTimeId { get; set; }
        public DateTime ReservedTime { get; set; }
    }
}