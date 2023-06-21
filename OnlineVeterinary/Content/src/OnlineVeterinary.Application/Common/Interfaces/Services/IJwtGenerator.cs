using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.Auth.Register;

namespace OnlineVeterinary.Application.Common.Interfaces.Services
{
    public interface IJwtGenerator
    {
        Jwt GenerateToken(User user);
    }
}