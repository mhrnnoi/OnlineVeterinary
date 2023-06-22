using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Domain.Role.Enums;

namespace OnlineVeterinary.Application.Common.Interfaces
{
       public record User(string FirstName, string LastName, string Email, string Password, RoleType RoleType);

}