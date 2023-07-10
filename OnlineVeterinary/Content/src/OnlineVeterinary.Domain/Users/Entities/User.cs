using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.Role.Enums;

namespace OnlineVeterinary.Domain.Users.Entities
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("LastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Column("Password")]
        public string Password { get; set; } = string.Empty;

        // there is no password salt and hash for simplicity for dev
        [Column("Role")]
        public string Role { get; set; } = string.Empty;
    }

}