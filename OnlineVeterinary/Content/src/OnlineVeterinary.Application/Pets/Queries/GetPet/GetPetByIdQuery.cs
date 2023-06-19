using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Queries.GetPet
{
    public record GetPetByIdQuery(Guid Id) : IRequest<PetDTO>;
    
}
