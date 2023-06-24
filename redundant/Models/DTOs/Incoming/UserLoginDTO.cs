using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string EmailOrUserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}