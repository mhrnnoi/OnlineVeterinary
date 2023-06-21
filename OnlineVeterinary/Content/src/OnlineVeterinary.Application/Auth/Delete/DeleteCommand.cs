using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Auth.Delete
{
    public record DeleteCommand(string Email, string Password, int RoleType) : IRequest<ErrorOr<string>>;
   
}