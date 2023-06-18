using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Commands.Update
{
    public record UpdateCareGiverCommand(Guid Id, string FirstName, string LastName, string Email, string Password) : IRequest<CareGiverDTO>;
    
}
