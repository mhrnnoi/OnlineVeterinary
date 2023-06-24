using ErrorOr;
using MediatR;

namespace OnlineVeterinary.Application.Features.Pets.Commands.Add
{
    public record AddPetCommand(
        string Name,
        DateTime DateOfBirth,
        int PetType,
        Guid CareGiverId,
        string CareGiverLastName) : IRequest<ErrorOr<string>>;
   
}
