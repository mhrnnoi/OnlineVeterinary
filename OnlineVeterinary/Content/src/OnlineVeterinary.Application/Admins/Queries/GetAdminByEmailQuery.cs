using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.Admins.Queries
{
    public record GetAdminByEmailQuery(string Email) : IRequest<ErrorOr<User>>;
    
}