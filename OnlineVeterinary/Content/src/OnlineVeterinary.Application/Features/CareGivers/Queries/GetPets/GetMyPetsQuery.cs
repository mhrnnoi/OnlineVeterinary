using ErrorOr;
using MediatR;
using OnlineVeterinary.Application.Features.DTOs;

namespace OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets
{
    public record GetMyPetsQuery(string Id) : IRequest<ErrorOr<List<PetDTO>>>;

}
