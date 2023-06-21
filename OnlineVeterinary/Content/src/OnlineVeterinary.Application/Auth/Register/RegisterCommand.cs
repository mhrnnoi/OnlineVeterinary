using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Auth.Common;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.Register
{
    public record RegisterCommand(string FirstName, string LastName, string Email, string Password, int RoleType) : IRequest<ErrorOr<AuthResult>>;

   
}