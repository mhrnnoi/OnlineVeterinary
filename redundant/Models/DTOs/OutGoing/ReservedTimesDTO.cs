using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs.OutGoing
{
    public class ReservedTimesDTO
    {
        public Guid Code { get; set; }
        public DateTime ReservedTime { get; set; }
        public string DrUserName { get; set; }
        public string PetUserName { get; set; }
        public string CareGiverUserName { get; set; }
    }
}