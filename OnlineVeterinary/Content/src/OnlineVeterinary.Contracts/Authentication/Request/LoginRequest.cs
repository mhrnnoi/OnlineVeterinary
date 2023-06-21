using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Contracts.Authentication.Request
{
    public record LoginOrDeleteRequest(string Email, string Password, int RoleType);
    
}