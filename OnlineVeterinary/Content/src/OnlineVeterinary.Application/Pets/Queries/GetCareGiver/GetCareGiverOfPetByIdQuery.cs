using System;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.Pets.Queries.GetCareGiver
{
    public record GetCareGiverOfPetByIdQuery(Guid Id) : IRequest<CareGiverDTO>;
    
}
