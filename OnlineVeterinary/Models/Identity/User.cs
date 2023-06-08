using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineVeterinary.Models.Identity
{
    public class User : IdentityUser
    {
        [Required]
        public bool IsDr { get; set; }
    }
}