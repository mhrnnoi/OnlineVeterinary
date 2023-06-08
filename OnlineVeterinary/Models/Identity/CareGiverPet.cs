using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.Identity
{
    public class CareGiverPet
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int CareGiverId { get; set; }
    }
}