using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Contracts.Authentication.Request
{
    public record RegisterRequest(string FirstName,
                                  string LastName,
                                  string Email,
                                  string Password,
                                  int Role);
   
}