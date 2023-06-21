using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Application.Common.Interfaces
{
       public record User(string FirstName, string LastName, string Email, string Password, int RoleType);

}