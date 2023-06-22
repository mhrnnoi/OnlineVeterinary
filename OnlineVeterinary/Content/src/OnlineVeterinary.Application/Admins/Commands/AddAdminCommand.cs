using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Common.Interfaces;

namespace OnlineVeterinary.Application.Admin.Commands
{
    public record AddAdminCommand() : IRequest<ErrorOr<User>>;
    
}