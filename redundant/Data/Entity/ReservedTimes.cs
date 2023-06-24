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
        public DateTime ReservedTime { get; set; }
        public string DrUserName { get; set; }
        public string PetUserName { get; set; }
        public string CareGiverUserName { get; set; }
        public Guid Code { get; set; }
    }
}