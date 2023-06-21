using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.Login
{
    public record LoginCommand(string Email, string Password, int RoleType) : IRequest<ErrorOr<AuthResult>>;
    
}