using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Commands.Update
{
    public record UpdatePetCommand(Guid Id, string Name,
        DateOnly DateOfBirth,
        int PetType) : IRequest<PetDTO>;
    
}
