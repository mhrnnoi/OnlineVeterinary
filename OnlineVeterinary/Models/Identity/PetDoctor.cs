using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.Identity
{
    public class PetDoctor
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int Doctor { get; set; }
    }
}