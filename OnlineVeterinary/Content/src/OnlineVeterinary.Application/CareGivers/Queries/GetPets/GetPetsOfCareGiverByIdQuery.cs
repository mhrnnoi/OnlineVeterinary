using System;
using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.DTOs;

namespace OnlineVeterinary.Application.CareGivers.Queries.GetPets
{
    public record GetPetsOfCareGiverByIdQuery(string Id) : IRequest<ErrorOr<List<PetDTO>>>;

}
