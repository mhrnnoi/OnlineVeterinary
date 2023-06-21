using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Auth.ChangePassword
{
    public record ChangePasswordCommand(string Email, string Password) : IRequest<ErrorOr<CareGiverDTO>>;
    
}